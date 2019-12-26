#pragma once
#include <string>

class Utils
{
private:
	void DeleteDebugCode(std::string& code);
	std::string ReadCode();
	void CopyToClipboard(const std::string& code);

public:
	std::string ReadFile(const std::wstring& filePath);
	void CopyCode();
	void OpenBrowser(const std::string& problemNumber);
};
