
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

//const int INF = 1 << 30;
const int INF = 9999;

int main(){
#ifdef _DEBUG
	//freopen("input.txt", "r", stdin);
	//freopen("output.txt", "w+", stdout);
#endif
	//freopen("StackingPlates.in", "r", stdin);
	freopen("input.txt", "r", stdin);
	freopen("output.txt", "w+", stdout);
	int stackCount;
	int caseNumber = 0;
	while (cin >> stackCount)
	{
		vector<set<int> > stackSet(stackCount);
		vector<vector<int> > stack(stackCount);
		vector<vector<int> > mergeMark(stackCount);
		map<int, int> countNum;
		vector<int> nums;
		int list[60 * 60] = { 0 };
		int cntList = 0;
		REP(i, stack.size())
		{
			int H, tmp;
			cin >> H;
			REP(j, H)
			{
				cin >> tmp;
				if (stackSet[i].find(tmp) == stackSet[i].end())
				{
					stackSet[i].insert(tmp);
					stack[i].push_back(tmp);
					mergeMark[i].push_back(0);
				}
				if (countNum.find(tmp) == countNum.end())
				{
					countNum[tmp] = 1;
					nums.push_back(tmp);
				}
				else
				{
					countNum[tmp] += 1;
				}
			}
		}

		sort(nums.begin(), nums.end());

		int MarkNo = 0;

		REP(n, nums.size())
		{
			int prevNum = ((n == 0) ? -1 : nums[n - 1]);
			int currNum = nums[n];
			if (countNum[currNum] != 1) continue;

			REP(i, stack.size())
			{
				if (stackSet[i].find(currNum) == stackSet[i].end()) continue;

				int currIndex;
				REP(j, stack[i].size()) if (stack[i][j] == currNum) { currIndex = j; break; }
				if (mergeMark[i][currIndex] != 0) continue;

				++MarkNo;
				mergeMark[i][currIndex] = MarkNo;
				if (currIndex != 0 && mergeMark[i][currIndex - 1] == 0 && stack[i][currIndex - 1] == prevNum) mergeMark[i][currIndex - 1] = MarkNo;
				FOR(j, currIndex + 1, stack[i].size())
				{
					if (countNum[stack[i][j]] == 1 && nums[n + (j - currIndex)] == stack[i][j])
					{
						mergeMark[i][j] = MarkNo;
					}
					else if (nums[n + (j - currIndex)] == stack[i][j])
					{
						mergeMark[i][j] = MarkNo;
						break;
					}
				}
				break;
			}
		}

		REP(n, nums.size())
		{
			int prevNum = ((n > 0) ? nums[n - 1] : -1);
			int currNum = nums[n];
			int nextNum = ((n < nums.size() - 1) ? nums[n + 1] : -1);

			REP(i, stack.size())
			{
				if (stackSet[i].find(currNum) == stackSet[i].end()) continue;

				if (stack[i].size() == 1)
				{
					mergeMark[i][0] = ++MarkNo;
					continue;
				}
				REP(j, stack[i].size())
				{
					if (stack[i][j] != currNum) continue;
					if (j == 0)
					{
						if (stack[i][j + 1] != nextNum)
						{
							mergeMark[i][j] = ++MarkNo;
						}
					}
					else if (j == stack[i].size() - 1)
					{
						if (stack[i][j - 1] != prevNum)
						{
							mergeMark[i][j] = ++MarkNo;
						}
					}
					else
					{
						if (stack[i][j - 1] != prevNum && stack[i][j + 1] != nextNum)
						{
							mergeMark[i][j] = ++MarkNo;
						}
					}
				}
			}
		}

		REP(n, nums.size() - 1)
		{
			int currNum = nums[n];
			int nextNum = nums[n + 1];
			bool isMark = false;
			bool isEndMark = false;
			bool isBeginMark = false;

			if (countNum[currNum] == 1) continue;

			REP(i, stack.size())
			{
				if (stackSet[i].find(currNum) == stackSet[i].end()) continue;

				int currIndex;
				REP(j, stack[i].size()) if (stack[i][j] == currNum) { currIndex = j; break; }

				if (mergeMark[i][currIndex] != 0)
					isMark = true;
				if (currIndex < stack[i].size() - 1 && mergeMark[i][currIndex + 1] == mergeMark[i][currIndex])
				{
					if (mergeMark[i][currIndex] != 0 && currIndex != 0 && mergeMark[i][currIndex - 1] != mergeMark[i][currIndex])
						isBeginMark = true;
					//if (mergeMark[i][currIndex] != 0 && currIndex == 0 && stack[i].size() > 1 && mergeMark[i][currIndex + 1] == mergeMark[i][currIndex])
					if (mergeMark[i][currIndex] != 0 && currIndex == 0 && stack[i].size() > 1 && mergeMark[i][currIndex + 1] == mergeMark[i][currIndex])
						isBeginMark = true;
				}
				if (mergeMark[i][currIndex] != 0 && currIndex != stack[i].size() - 1 && mergeMark[i][currIndex + 1] != mergeMark[i][currIndex])
					isEndMark = true;
				if (mergeMark[i][currIndex] != 0 && currIndex == stack[i].size() - 1)
					isEndMark = true;
			}

			if (!isBeginMark && isEndMark || !isMark)
			{
				// 연속된거 한쌍 찾아서 마크한다
				REP(i, stack.size())
				{
					if (stackSet[i].find(currNum) == stackSet[i].end()) continue;
					if (stackSet[i].find(nextNum) == stackSet[i].end()) continue;

					int currIndex;
					REP(j, stack[i].size()) if (stack[i][j] == currNum) { currIndex = j; break; }

					if (mergeMark[i][currIndex] != 0) continue;
					if (mergeMark[i][currIndex + 1] != 0) continue;

					++MarkNo;
					mergeMark[i][currIndex] = MarkNo;
					mergeMark[i][currIndex + 1] = MarkNo;

					break;

				}
			}
		}
		int SplitCount = 0;
		int JoinCount = 0;
		int result = -1;
		REP(i, stack.size())
		{
			JoinCount += 1;
			result += 1;
			FOR(j, 1, stack[i].size())
			{
				if (mergeMark[i][j] == 0 || mergeMark[i][j - 1] != mergeMark[i][j])
				{
					++SplitCount;
					JoinCount += 1;
					result += 2;
				}
			}
		}
		//cout << result << endl;
		cout << "Case " << ++caseNumber << ": " << result << endl;
	}

	return 0;
}

