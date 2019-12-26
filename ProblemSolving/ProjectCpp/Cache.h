#pragma once
#include <string>
#include <vector>
#include "InputOutput.h"

class Cache
{
private:
	std::string GetCacheFilePath(const std::string& problemNumber);
public:
	void SaveInputOutput(const std::string& problemNumber, const std::vector<InputOutput>& ioList);
	bool ExistsInputOutput(const std::string& problemNumber);
	std::vector<InputOutput> GetInputOutput(const std::string& problemNumber);
};


