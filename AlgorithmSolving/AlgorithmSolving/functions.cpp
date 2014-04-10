

int power(int x, int n){
	int retval = 1;
	int factor = x;
	while (n != 0){
		if ((n & 1) != 0)
			retval *= factor;
		factor *= factor;
		n >>= 1;
	}
	return retval;
}

unsigned int _sqrt(unsigned int a){
	int rem = 0, root = 0, divisor = 0;
	//16 : 32bit / 2
	for (int i = 0; i<16; i++){
		root <<= 1;
		//30 : 32bit - 2
		rem = ((rem << 2) + (a >> 30));
		a <<= 2;
		divisor = (root << 1) + 1;
		if (divisor <= rem){
			rem -= divisor;
			root++;
		}
	}
	return root;
}

bool isPrime(long long n) {
	if (n < 2) return false;
	for (long long i = 2; i*i <= n; i++)
		if (n%i == 0) return false;
	return true;
}

int gcd(int a, int b) {
	int r;
	while (b>0){
		r = a % b;
		a = b;
		b = r;
	}
	return a;
}

int gcd(int a, int b){
	if (b == 0) return a;
	return gcd(b, a % b);
}

int lcm(int a, int b) {
	return (a*b) / gcd(a, b);
}

map<int, int> factorize(int N) {
	map<int, int> factors;
	for (int i = 2; i*i <= N; i++) {
		while (N%i == 0) {
			factors[i]++;
			N /= i;
		}
	}
	if (N>1) factors[N]++;
	return factors;
}

//////////////////////////////////////////////////////////////////////////
// KMP 알고리즘 여기서부터
//////////////////////////////////////////////////////////////////////////
void failure(int *ff, string &pattern) {
	ff[0] = 0;
	int i = 1, j = 0;
	while (i<(int)pattern.size()) {
		if (pattern[i] == pattern[j])
			ff[i++] = ++j;
		else if (j>0) j = ff[j - 1];
		else ff[i++] = 0;
	}
}

int kmp(string &seq, string &pattern) {
	int *ff = new int[pattern.size()];
	failure(ff, pattern);

	int i = 0, j = 0;
	while (i<(int)seq.size()) {
		if (seq[i] == pattern[j]){
			if (j == pattern.size() - 1){
				return i - j + 1;
			}
			else { i++; j++; }
		}
		else {
			if (j>0) j = ff[j - 1];
			else i++;
		}
	}

	delete[]ff;
	return -1;
}


//////////////////////////////////////////////////////////////////////////
// KMP 알고리즘 여기까지
//////////////////////////////////////////////////////////////////////////



//////////////////////////////////////////////////////////////////////////
//  Boyer Moore Algorithm 여기서부터
//////////////////////////////////////////////////////////////////////////

void lastOccurrence(int *lo, string &pattern) {
	//initialize
	for (int i = 0; i<10; i++) lo[i] = -1;
	for (int i = 0; i<pattern.size(); i++) {
		lo[pattern[i] - '0'] = i;
	}
}
int boyerMoore(string &seq, string &pattern) {

	int *lo = new int[10];
	lastOccurrence(lo, pattern);

	int m = pattern.size();
	int i = m - 1;
	int j = i;

	while (i<seq.size()) {
		if (seq[i] == pattern[j]) {
			if (j == 0) return i + 1;
			else { i--; j--; }
		}
		else {
			int last = lo[seq[i] - '0'];
			i = i + m - min(j, 1 + last);
			j = m - 1;
		}
	}
	delete[]lo;
	return -1;
}

//////////////////////////////////////////////////////////////////////////
//  Boyer Moore Algorithm 여기까지
//////////////////////////////////////////////////////////////////////////


버블 정렬
vector<int> bubbleSort(vector<int> a) {
	for (int i = 0; i<a.size(); i++) {
		for (int j = 0; j<a.size() - i - 1; j++) {
			if (a[j]>a[j + 1])
				swap(a[j], a[j + 1]);
		}
	}
	return a;
}


선택 정렬
vector<int> selectSort(vector<int> a) {
	for (int i = 0; i<a.size(); i++) {
		int min = 1000, minIndex = 0;
		for (int j = i; j<a.size(); j++) {
			if (min>a[j]) {
				min = a[j];
				minIndex = j;
			}
		}
		swap(a[i], a[minIndex]);
	}
	return a;
}

삽입정렬
vector<int> insertSort(vector<int> a) {
	for (int i = 0; i<a.size(); i++) {
		for (int j = 0; j<i; j++) {
			if (a[i]<a[j])
				swap(a[i], a[j]);
		}
	}
	return a;
}


머지소트
void mergeSort(vector<int> &a, int f, int m, int l) {

	if (f == l) return;

	mergeSort(a, f, (f + m) / 2, m);
	mergeSort(a, m + 1, (m + l + 1) / 2, l);

	vector<int> left, right;
	for (int i = f; i <= m; i++) left.push_back(a[i]);
	for (int i = m + 1; i <= l; i++) right.push_back(a[i]);

	//left.push_back(1000);
	//right.push_back(1000);

	int k = 0, t = 0;
	for (int i = f; i <= l; i++) {
		if (t == right.size() || (k != left.size() && left[k] <= right[t])){
			a[i] = left[k]; k++;
		}
		else {
			a[i] = right[t]; t++;
		}
	}
}



퀵 소트
void quickSort(vector<int> &a, int f, int l) {

	if (f >= l) return;
	int pivot = rand() % (l - f + 1) + f;
	int i = f - 1, x = a[pivot];

	swap(a[l], a[pivot]);
	for (int j = f; j<l; j++) {
		if (a[j] <= x){
			swap(a[++i], a[j]);
		}
	}
	swap(a[l], a[i + 1]);
	quickSort(a, f, i);
	quickSort(a, i + 2, l);
}





스트링 곱

string multifly(string a, string b) {

	int remain, subRes;
	string res(a.size() + b.size() - 1, '0');

	REP(i, a.size()) {

		remain = 0;
		REP(j, b.size()) {
			subRes = remain + res[res.size() - i - j - 1] - '0'
				+ (a[a.size() - i - 1] - '0')*(b[b.size() - j - 1] - '0');

			if ((int)log10((double)subRes) + 1 == 2) {
				remain = (subRes / 10);
				res[res.size() - i - j - 1] = subRes % 10 + '0';
			}
			else {
				remain = 0;
				res[res.size() - i - j - 1] = subRes + '0';
			}
		}
		if (remain != 0) {
			if (i == a.size() - 1)
				res.insert(res.begin(), remain + '0');
			else res[res.size() - b.size() - i - 1] = remain + '0';
		}
	}
	return res;
}





스트링 합
string add(string a, string b) {

	int remain = 0, subRes;
	string res;

	if (a.size()>b.size()) {
		res.resize(a.size(), '0');
		REP(i, b.size()) res[a.size() - i - 1] = b[b.size() - i - 1];
		b = res;
	}
	else if (a.size()<b.size()) {
		res.resize(b.size(), '0');
		REP(i, a.size()) res[b.size() - i - 1] = a[a.size() - i - 1];
		a = res;
	}
	else {
		res.resize(a.size(), '0');
	}


	REPD(i, a.size()){
		subRes = remain + a[i] + b[i] - 2 * '0';
		if ((int)log10((double)subRes) + 1 == 2) {
			ans++;
			remain = (subRes / 10);
			res[i] = (subRes) % 10 + '0';
		}
		else {
			remain = 0;
			res[i] = subRes + '0';
		}
	}
	if (remain != 0) res.insert(res.begin(), '1');
	return res;
}





Dijkstra Algorihm

typedef pair<int, int> ii;

vector<int> D(N, 1 << 20);
priority_queue<ii, vector<ii>, greater<ii> > Q;

D[0] = 0;
Q.push(ii(0, 0));

while (!Q.empty()) {
	ii top = Q.top();
	Q.pop();
	int v = top.second, d = top.first;
	if (d <= D[v]) {
		for (int i = 0; G[v].size(), i++) {
			int v2 = G[v][i].first, cost = G[v][i].second;
			if (D[v2] > D[v] + cost) {
				D[v2] = D[v] + cost;
				Q.push(ii(D[v2], v2));
			}
		}
	}
}



Prim Algorithm

vector<double> D(N, 1 << 20);
vector<int> done(N, 0);
priority_queue<di, vector<di>, greater<di> > Q;
double ans = 0;
Q.push(di(0, 0));
D[0] = 0;

while (!Q.empty()) {
	di top = Q.top();
	Q.pop();
	int v = top.second;
	double d = top.first;

	if (done[v]) continue;
	done[v] = 1;
	ans += d;

	REP(i, graph[v].size()) {
		int v2 = graph[v][i].second;
		double cost = graph[v][i].first;
		if (D[v2] > cost) {
			D[v2] = cost;
			Q.push(di(D[v2], v2));
		}
	}
}


Kruskal

map<string, int> idx;
int p[55], rank[55];

struct EDGE {
	string id;
	int from, to, weight;
};

bool operator <(EDGE e1, EDGE e2) {
	if (e1.weight != e2.weight) return e1.weight>e2.weight;
	return e1.id>e2.id;
}

//kruskal
void create_set(int x) {
	p[x] = x;
	rank[x] = 1;
}
int find_set(int x) {
	if (p[x] != x) p[x] = find_set(p[x]);
	return p[x];
}
void merge_sets(int x, int y) {
	int px = find_set(x);
	int py = find_set(y);
	if (rank[px]>rank[py]) p[py] = px;
	else p[px] = py;
	if (rank[px] == rank[py]) rank[py]++;
}

priority_queue<EDGE> pq;

//에지 연결 코드 추가

while (pq.size() != 0) {
	EDGE e = pq.top(); pq.pop();
	if (find_set(e.from) != find_set(e.to)) {
		merge_sets(e.from, e.to);
		res.push_back(e.id);
	}
}


///////////////////////////////////////////////////////////
// Treap
///////////////////////////////////////////////////////////

struct Node{
	KeyType key;
	int priority, size;

	Node *left, *right;
	Node(const KeyType& _key) : key(_key), priority(rand()), size(1), left(NULL), right(NULL){
	}
	void setLeft(Node* newLeft){ left = newLeft; calcSize(); }
	void setRight(Node* newRight){ left = newRight; calcSize(); }

	void calcSize(){
		size = 1;
		if (left) size += left->size;
		if (right) size += right->size;
	}
};


///////////////////////////////////////////////////////////




class Exp{
public:
	int num, exp;
	Exp(){ num = exp = 0; }
	Exp(int n, int e){ num = n; exp = e; }
};
class NumberTheory{
	/*
	<<함수 설명>>
	- getPrime(int N) : N이하의 소수를 primes에 넣는다. (에라토스테네스의 체)
	- factorizeInt(int num, V<Exp>& rst) : num을 소인수분해 해서 rst에 모은다.
	- factorizeArray(V<int>& v, V<Exp>& rst) : v의 수들을 소인수분해 해서 rst에 모은다.
	- getNumVecExp(V<Exp>& vecExp) : vecExp의 값을 구한다.
	- power(int x, int n) : x의 n승을 구한다. O(log n)
	- combin(int N, int R) : 조합 nCr 값을 소인수분해 방식으로 구한다.
	- getDivs(int num, V<int>& divs) : num의 약수를 소인수분해 방식으로 구한다.
	- getDivsCnt(int num) : num의 약수의 갯수를 구한다.
	- gcd(int a, int b) : a, b의 최대공약수를 구한다.
	- lcm(int a, int b) : a, b의 최소공배수를 구한다.
	- gcd(V<int>& arr) : arr의 최대공약수를 구한다.
	- lcm(V<int>& arr) : arr의 최소공배수를 구한다.
	- isPrime(long long p) : p의 소수 판별 (test : 99194853094755497LL)
	*/
public:
	vector<int> primes;

	//N이하의 소수를 primes에 넣는다.
	//에라토스테네스의 체 방식으로 구현함.
	void getPrime(int N){
		vector<bool> isPrime(N + 1, true);
		primes.clear();
		FOR1(i, 2, N){
			if (isPrime[i] == true){
				primes.push_back(i);
				for (int j = i * 2; j <= N; j += i) isPrime[j] = false;
			}//end if
		}//for i
	}//getPrime

	//num을 수인수분해 해서 rst에 넣는다.
	//before : getPrime()
	void factorizeInt(int num, vector<Exp>& rst){
		rst.clear();
		rst.resize(primes.size());
		REP(i, rst.size()) rst[i].num = primes[i];
		while (num != 1){
			REP(p, rst.size()){
				while ((num%rst[p].num) == 0){
					num /= rst[p].num;
					++rst[p].exp;
				}//while
				if (num == 1) break;
			}//for p
		}//while
	}//factorizeInt

	//v의 수들을 소인수분해 해서 rst에 모은다.
	//before : getPrime()
	void factorizeArray(const vector<int>& v, vector<Exp>& rst){
		rst.clear();
		rst.resize(primes.size());
		REP(i, rst.size()) rst[i].num = primes[i];
		REP(i, v.size()){
			int n = v[i];
			while (n != 1){
				REP(p, rst.size()){
					while ((n%rst[p].num) == 0){
						n /= rst[p].num;
						++rst[p].exp;
					}//while
					if (n == 1) break;
				}//for p
			}//while
		}//for i
	}//factorizeArray

	//vecExp의 값을 구한다.
	int getNumVecExp(vector<Exp>& vecExp){
		int rst = 1;
		REP(i, vecExp.size()){
			rst *= power(vecExp[i].num, vecExp[i].exp);
		}//for i
		return rst;
	}

	//x의 n승을 구한다.
	int power(int x, int n){
		int retval = 1;
		int factor = x;
		while (n != 0){
			if ((n & 1) != 0)
				retval *= factor;
			factor *= factor;
			n >>= 1;
		}//while
		return retval;
	}//power

	//조합 nCr 값을 소인수분해 방식으로 구한다.
	//before : getPrime()
	int combin(int N, int R){
		R = min(R, N - R);
		if (R == 0) return 1;
		if (R == 1) return N;
		vector<int> vecN(R);
		vector<int> vecR(R);
		REP(i, R) vecN[i] = i + N - R + 1;
		REP(i, R) vecR[i] = i + 1;
		vector<Exp> vecExpN, vecExpR;
		factorizeArray(vecN, vecExpN);
		factorizeArray(vecR, vecExpR);
		REP(i, vecExpN.size()) vecExpN[i].exp -= vecExpR[i].exp;
		return getNumVecExp(vecExpN);
	}//combin


	int combin2(int n, int r)
	{
		long long rst = 1;
		r = min(r, n - r);

		REP(i, r) rst = (rst * (n - i)) / (i + 1);

		return (int)rst;
	}

private:
	//getDivs 함수에서 호출하는 함수임. 재귀함수
	//특별히 사용자가 직접 호출할 일은 없음..
	void getDivsRec(int index, int div, const vector<Exp>& vecExp, vector<int>& divs){
		bool flag = false;
		FOR(i, index, vecExp.size()){
			if (vecExp[i].exp > 0){
				flag = true;
				REP(j, vecExp[i].exp + 1){
					getDivsRec(i + 1, div*power(vecExp[i].num, j), vecExp, divs);
				}//for j
				break;
			}//end if
		}//for i
		if (flag == false) divs.push_back(div);
	}//getDivsRec

public:
	//num의 약수를 소인수분해 방식으로 구한다.
	//before : getPrime()
	void getDivs(int num, vector<int>& divs){
		vector<Exp> vecExp;
		factorizeInt(num, vecExp);
		getDivsRec(0, 1, vecExp, divs);
		sort(divs.begin(), divs.end());
	}//getDivs

	//약수의 갯수를 구함
	//before : getPrime()
	int getDivsCnt(int num){
		vector<Exp> vecExp;
		factorizeInt(num, vecExp);
		int rst = 1;
		REP(i, vecExp.size()) if (vecExp[i].exp > 0) rst *= (vecExp[i].exp + 1);
		return rst;
	}//getDivsCnt

	//a, b의 최대공약수를 구한다.
	int gcd(int a, int b) {
		int r;
		while (b>0){
			r = a % b;
			a = b;
			b = r;
		}//while
		return a;
	}//gcd

	//a, b의 최소공배수를 구한다.
	int lcm(int a, int b) {
		return (a / gcd(a, b))*b;
	}//lcm

	//arr의 최대공약수를 구한다.
	int gcd(const vector<int>& arr){
		int rst = arr[0];
		FOR(i, 1, arr.size()) rst = gcd(rst, arr[i]);
		return rst;
	}//gcd

	//arr의 최소공배수를 구한다.
	int lcm(const vector<int>& arr){
		int rst = arr[0];
		FOR(i, 1, arr.size()) rst = lcm(rst, arr[i]);
		return rst;
	}//lcm
};//NumberTheory