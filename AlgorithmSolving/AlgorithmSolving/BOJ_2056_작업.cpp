
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

class Work{
public:
	int Index;
	int WorkingTime;
	int StartTime;
	int EndTime;
	int CntDependent; //내가 물고있는
	vector<int> ToDependent; // 내가 물고있는
	vector<int> FromDependent; // 나한테 물려있는
};

class Item{
public:
	int Index;
	int EndTime;

	Item(){}

	Item(int Index, int EndTime){
		this->Index = Index;
		this->EndTime = EndTime;
	}

	bool operator < (const Item& b) const {
		if (EndTime != b.EndTime) return EndTime < b.EndTime;
		return Index < b.Index;
	}

	bool operator >(const Item& b) const {
		if (EndTime != b.EndTime) return EndTime > b.EndTime;
		return Index > b.Index;
	}
};

int main(){
#ifdef _DEBUG
	freopen("input.txt", "r", stdin);
#endif
	int tmp;
	int N;
	cin >> N;
	vector<Work> work(N);
	priority_queue<Item, vector<Item>, greater<Item> > pq;
	REP(i, N) {
		work[i].Index = i;
		cin >> work[i].WorkingTime;
		cin >> work[i].CntDependent;
		REP(j, work[i].CntDependent) {
			cin >> tmp;
			work[i].ToDependent.push_back(tmp - 1);
			work[tmp - 1].FromDependent.push_back(i);
		}
		if (work[i].CntDependent == 0){
			pq.push(Item(i, work[i].WorkingTime));
		}
	}

	int currTime;
	while (!pq.empty()){
		Item currItem = pq.top();
		pq.pop();
		currTime = currItem.EndTime;
		Work& currWork = work[currItem.Index];
		REP(i, currWork.FromDependent.size()){
			Work& targetWork = work[currWork.FromDependent[i]];
			--targetWork.CntDependent;
			if (targetWork.CntDependent == 0){
				pq.push(Item(currWork.FromDependent[i], currTime + targetWork.WorkingTime));
			}
		}
	}
	cout << currTime << endl;
	return 0;
}

