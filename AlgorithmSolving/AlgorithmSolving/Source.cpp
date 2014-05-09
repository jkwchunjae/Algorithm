
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
const int INT__MIN = 1 << 31;
const int INT__MAX = 0x7fffffff;

struct Point{
	int x, y;
};

bool CompareX(const Point& p1, const Point& p2)
{
	if (p1.x != p2.x) return p1.x < p2.x;
	return p1.y < p2.y;
}

bool CompareY(const Point& p1, const Point& p2)
{
	if (p1.y != p2.y) return p1.y < p2.y;
	return p1.x < p2.x;
}

Point pset[100010];

int calcDist(int pp1, int pp2)
{
	Point& p1 = pset[pp1];
	Point& p2 = pset[pp2];
	return (p1.x - p2.x) * (p1.x - p2.x) + (p1.y - p2.y) * (p1.y - p2.y);
}

int naiveSolution(int begin, int end)
{
	int rst = INT__MAX;
	FOR(i, begin, end)
		FOR1(j, i + 1, end)
			rst = min(rst, calcDist(i, j));
	return rst;
}

int closestPairProblem(int begin, int end)
{
	if (end - begin + 1 <= 5)
	{
		return naiveSolution(begin, end);
	}

	int d = INT__MAX;
	int mid = begin + (end - begin) / 2;

	d = min(d, closestPairProblem(begin, mid));
	d = min(d, closestPairProblem(mid + 1, end));

	int leftBegin = mid;
	while (calcDist(leftBegin, mid + 1) <= d) --leftBegin;
	int rightEnd = mid + 1;
	while (calcDist(rightEnd, mid) <= d) ++rightEnd;

	sort(&pset[leftBegin], &pset[mid + 1], CompareY);
	sort(&pset[mid + 1], &pset[rightEnd + 1], CompareY);

	int rightIndex = mid + 1;
	FOR1(leftIndex, leftBegin, mid)
	{
		Point& leftPoint = pset[leftIndex];
		while (true)
		{
			if (rightIndex == mid + 1) break;
			if (!(leftPoint.y >= pset[rightIndex].y
				&& (leftPoint.y - pset[rightIndex].y) * (leftPoint.y - pset[rightIndex].y) <= d))
			{
				break;
			}
			--rightIndex;
		}
		while (rightIndex <= rightEnd)
		{
			Point& rightPoint = pset[rightIndex];
			if (leftPoint.y < rightPoint.y
				&& (leftPoint.y - rightPoint.y) * (leftPoint.y - rightPoint.y) > d)
					break;
			int currDist = calcDist(leftIndex, rightIndex);
			d = min(d, currDist);
			++rightIndex;
		}
	}

	return d;
}

int main(){
#ifdef _DEBUG
	freopen("input.txt", "r", stdin);
	//freopen("output.txt", "w+", stdout);
#endif
	int N;
	while (cin >> N)
	{
		REP(i, N) cin >> pset[i].x >> pset[i].y;
		sort(pset, &pset[N], CompareX);
		cout << closestPairProblem(0, N - 1) << endl;
	}
	return 0;
}

