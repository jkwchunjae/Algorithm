#define _CRT_SECURE_NO_WARNINGS

#include <iostream>
#include <cstdio>
#include <iomanip>
#include <cmath>
#include <vector>
#include <algorithm>
#include <functional>
#include <string>
#include <cstring>
#include <sstream>
#include <fstream>
#include <cstdlib>
#include <stack>
#include <queue>
#include <map>
#include <set>
#include <ctime>
#include <cassert>

using namespace std;

#define REP(v, hi) for (int v=0;v<(hi);v++)
#define REPD(v, hi) for (int v=((hi)-1);v>=0;v--)
#define FOR(v, lo, hi) for (int v=(lo);v<(hi);v++)
#define FORD(v, lo, hi) for (int v=((hi)-1);v>=(lo);v--)
#define REP1(v, hi) for (int v=1;v<=(hi);v++)
#define REPD1(v, hi) for (int v=(hi);v>=1;v--)
#define FOR1(v, lo, hi) for (int v=(lo);v<=(hi);v++)
#define FORD1(v, lo, hi) for (int v=(hi);v>=(lo);v--)

const double eps = 1 / (double)1000000000;

class Obj {
public:
	int DeadLine;
	int Cup;

	bool operator < (const Obj& b) const {
		if (Cup != b.Cup) return Cup < b.Cup;
		return DeadLine < b.DeadLine;
	}

	bool operator >(const Obj& b) const {
		if (Cup != b.Cup) return Cup > b.Cup;
		return DeadLine > b.DeadLine;
	}
};

int main(){
#ifdef _DEBUG
	freopen("input.txt", "r", stdin);
	//freopen("output.txt", "w+", stdout);
#endif
	int N;
	cin >> N;

	vector<Obj> obj(N);
	vector<int> Eat(N + 1);
	set<int> sett;

	REP(i, N) cin >> obj[i].DeadLine >> obj[i].Cup;
	REP(i, N) sett.insert(-(i + 1));

	sort(obj.begin(), obj.end(), greater<Obj>());

	REP(i, N)
	{
		auto it = sett.lower_bound(-obj[i].DeadLine);
		if (it == sett.end()) continue;
		Eat[-(*it)] = obj[i].Cup;
		sett.erase(it);
	}

	int result = 0;
	REP(i, N) result += Eat[i];

	cout << result << endl;

	return 0;
}