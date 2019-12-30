#pragma once
#include <Windows.h>
#include <string>
#include <vector>
#include "Cache.h"
#include "Utils.h"

struct MakeInputArgs
{
	std::string ProblemNumber;
	bool UseLocalInput;

	MakeInputArgs()
		: ProblemNumber(""),
		UseLocalInput(false)
	{
	}

	MakeInputArgs(const std::string& problemNumber)
		: ProblemNumber(problemNumber),
		UseLocalInput(false)
	{
	}
};

class IoManager
{
private:
	Cache _cache;

	struct ComInit
	{
		HRESULT hr;
		ComInit() : hr(::CoInitialize(nullptr)) {}
		~ComInit() { if (SUCCEEDED(hr)) ::CoUninitialize(); }
	};

	std::string GetHtml(std::string problemNumber);

public:
	IoManager();

	std::vector<InputOutput> MakeInputOutput(const MakeInputArgs& args);
};


