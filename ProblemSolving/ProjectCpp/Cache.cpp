#include "Cache.h"
#include <fstream>
#include "json.hpp"

std::string Cache::GetCacheFilePath(const std::string& problemNumber)
{
	return "Debug/" + problemNumber + ".json";
}

void Cache::SaveInputOutput(const std::string& problemNumber, const std::vector<InputOutput>& ioList)
{
	nlohmann::json j;
	for (auto i = 0; i < ioList.size(); i++)
	{
		j[i]["Number"] = ioList[i].Number;
		j[i]["Input"] = ioList[i].Input;
		j[i]["Output"] = ioList[i].Output;
	}

	auto jsonText = j.dump(2);
	auto filePath = GetCacheFilePath(problemNumber);
	std::ofstream fs(filePath, std::ofstream::out);
	fs << jsonText;
}

bool Cache::ExistsInputOutput(const std::string& problemNumber)
{
	auto filePath = GetCacheFilePath(problemNumber);
	std::ifstream ifile(filePath);
	return ifile.good();
}

std::vector<InputOutput> Cache::GetInputOutput(const std::string& problemNumber)
{
	std::vector<InputOutput> result;

	if (!ExistsInputOutput(problemNumber))
		return result;

	auto filePath = GetCacheFilePath(problemNumber);
	std::ifstream ifile(filePath);

	nlohmann::json j;
	ifile >> j;

	if (!j.is_array())
		return result;

	for (const auto& obj : j.array())
	{
		InputOutput io;
		io.Number = obj["Number"].get<int>();
		io.Input = obj["Input"].get<std::string>();
		io.Output = obj["Output"].get<std::string>();

		result.push_back(io);
	}

	return result;
}
