#include "IoManager.h"
#include <atlbase.h>  // CComPtr
#include <urlmon.h>
#include <fstream>

#pragma comment(lib, "urlmon.lib")

IoManager::IoManager()
	: _cache(Cache())
{ }


std::string IoManager::GetHtml(std::string problemNumber)
{
	ComInit init;
	CComPtr<IStream> pStream;

	std::wstring url(L"https://www.acmicpc.net/problem/");
	url.append(std::wstring(problemNumber.begin(), problemNumber.end()));

	URLOpenBlockingStreamW(nullptr, url.c_str(), &pStream, 0, nullptr);

	std::string html;
	DWORD bytesRead;
	do
	{
		char buffer[1024];
		pStream->Read(buffer, sizeof(buffer), &bytesRead);
		html.append(buffer, buffer + bytesRead);
	} while (bytesRead > 0);

	return std::string(html.begin(), html.end());
}

std::vector<InputOutput> IoManager::MakeInputOutput(const MakeInputArgs& args)
{
	if (args.UseLocalInput)
	{
		InputOutput inout;
		inout.Number = 0;

		Utils utils;
		inout.Input = utils.ReadFile(L"input.txt");
		inout.Output = utils.ReadFile(L"output.txt");

		return { inout };
	}

	Cache cache;
	if (cache.ExistsInputOutput(args.ProblemNumber))
	{
		return cache.GetInputOutput(args.ProblemNumber);
	}

	std::vector<InputOutput> result;

	auto html = GetHtml(args.ProblemNumber);

	auto startIndex = 0;
	while (true)
	{
		auto a = html.find("<pre", startIndex);
		if (a == -1)
			break;
		auto b = html.find(">", a);
		auto c = html.find("</pre>", a);
		auto preHeader = html.substr(a, b - a);
		auto preData = html.substr(b + 1, c - b - 1);

		if (preHeader.find("sampledata") == std::string::npos)
			continue;

		std::string type = preHeader.find("sample-input") != std::string::npos ? "sample-input"
			: preHeader.find("sample-output") != std::string::npos ? "sample-output" : "";

		if (type == "")
			continue;

		while (preData.find("\r") != std::string::npos)
			preData.erase(preData.find("\r"), 1);

		auto posType = preHeader.find(type);
		auto numberLength = preHeader.find("\"", posType) - posType - type.length() - 1;
		auto number = stoi(preHeader.substr(posType + type.length() + 1, numberLength));

		auto checkExists = std::find_if(result.begin(), result.end(), [number](const InputOutput& data) {
			return data.Number == number;
		});
		if (checkExists == result.end())
		{
			InputOutput inout;
			inout.Number = number;
			if (type == "sample-input")
				inout.Input = preData;
			else if (type == "sample-output")
				inout.Output = preData;
			result.push_back(inout);
		}
		else
		{
			if (type == "sample-input")
				checkExists->Input = preData;
			else if (type == "sample-output")
				checkExists->Output = preData;
		}

		startIndex = a + 1;
	}

	cache.SaveInputOutput(args.ProblemNumber, result);

	return result;
}
