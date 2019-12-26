#pragma once
#include <Windows.h>
#include <string>
#include <vector>
#include "Cache.h"

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

	std::vector<InputOutput> MakeInputOutput(const std::string& problemNumber);
};


