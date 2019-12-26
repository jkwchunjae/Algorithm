#include <iostream>
#include <string>
#include <vector>
#include <sstream>
#if _DEBUG // delete
#include "IoManager.h"
#include "Utils.h"
#endif

using namespace std;

void Solve(istream& in, ostream& out)
{
	int a = 0, b = 0;
	in >> a >> b;
	out << a + b << endl;
}

int main()
{
#if _DEBUG // delete
	string problemNumber = "1000";
	IoManager ioManager;
	auto ioList = ioManager.MakeInputOutput(problemNumber);

	bool checkAll = true;
	for (auto io : ioList)
	{
		stringstream input(stringstream::in | stringstream::out);
		input << io.Input;
		stringstream output(stringstream::out);

		Solve(input, output);

		auto accepted = output.str() == io.Output;

		if (!accepted)
		{
			cout << "[# " << io.Number << "]" << endl;
			cout << "[My output]" << endl << output.str() << endl;
			cout << "[Sample output]" << endl << io.Output << endl;
		}

		checkAll &= accepted;
	}

	if (checkAll)
	{
		cout << "Passed all sample inputs !!" << endl;
		CopyCode();
		OpenBrowser(problemNumber);
	}
#else
	Solve(cin, cout);
#endif
	return 0;
}

