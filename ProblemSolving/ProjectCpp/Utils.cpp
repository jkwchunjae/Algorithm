#include "Utils.h"
#include <windows.h>
#include <fstream>
#include <stdlib.h>
#include <stdio.h>

void Utils::DeleteDebugCode(std::string& code)
{
	auto a = code.find("#if _DEBUG // delete");
	if (a == std::string::npos)
		return;

	auto b = code.find("#else", a);
	auto c = code.find("#endif", a);

	if (c == std::string::npos)
		return;

	if (b == std::string::npos)
	{
		code = code.substr(0, a - 1) + code.substr(c + 6);
	}
	else
	{
		if (c < b)
		{
			/*
			#if
			#endif
			*/
			code = code.substr(0, a - 1) + code.substr(c + 6);
		}
		else
		{
			/*
			#if
			#else
			#endif
			*/
			code = code.substr(0, a - 1)
				+ code.substr(b + 5, c - b - 5)
				+ code.substr(c + 7);
		}
	}
}

std::string Utils::ReadFile(const std::wstring& filePath)
{
	std::ifstream iStream(filePath);

	std::string text, line;
	while (std::getline(iStream, line))
		text += line + "\n";

	return text;
}

std::string Utils::ReadCode()
{
	wchar_t  buffer[MAX_PATH];
	auto len = GetModuleFileNameW(NULL, buffer, MAX_PATH);

	std::wstring path(buffer, buffer + len);
	path = path.substr(0, path.find_last_of(L"/\\"));
	path = path.substr(0, path.find_last_of(L"/\\"));
	path += L"\\main.cpp";

	return ReadFile(path);
}

void Utils::CopyToClipboard(const std::string& code)
{
	if (OpenClipboard(NULL))
	{
		HGLOBAL clipbuffer;
		char* buffer;
		EmptyClipboard();
		clipbuffer = GlobalAlloc(GMEM_DDESHARE, code.size() + 1);
		buffer = (char*)GlobalLock(clipbuffer);
		std::strcpy(buffer, code.c_str());
		GlobalUnlock(clipbuffer);
		SetClipboardData(CF_TEXT, clipbuffer);
		CloseClipboard();
	}
}

void Utils::CopyCode()
{
	auto code = ReadCode();

	DeleteDebugCode(code); DeleteDebugCode(code); DeleteDebugCode(code);
	DeleteDebugCode(code); DeleteDebugCode(code); DeleteDebugCode(code);
	DeleteDebugCode(code); DeleteDebugCode(code); DeleteDebugCode(code);
	DeleteDebugCode(code); DeleteDebugCode(code); DeleteDebugCode(code);
	DeleteDebugCode(code); DeleteDebugCode(code); DeleteDebugCode(code);

	CopyToClipboard(code);
}

void Utils::OpenBrowser(const std::string& problemNumber)
{
	std::wstring str2(problemNumber.length(), L' ');
	std::copy(problemNumber.begin(), problemNumber.end(), str2.begin());
	std::wstring url(L"https://www.acmicpc.net/submit/");
	url += str2;
	ShellExecute(0, 0, url.c_str(), 0, 0, SW_SHOW);
}

