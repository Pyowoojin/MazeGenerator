#include <iostream>
#include <vector>
#include <stack>
#include <random>
using namespace std;

int dx[4] = { 1,0,-1,0 };
int dy[4] = { 0,1,0,-1 };

class cell {
public:
	int entrance;
	bool visited;

	cell() {
		// 뚫려있는 벽을 의미함.
		// ↑→↓← 순서로 1000, 0100, 0010, 0001
		entrance = 0x0000;
		// 방문 배열 의미
		visited = false;
	}
	~cell() {};

};

template <typename T>
void prt(vector <T>& vc) {
	for (int i = 0; i < vc.size(); i++) {
		for (int j = 0; j < vc[0].size(); j++) {
			cout << vc[i][j].entrance << " ";
		}
		cout << endl;
	}
}

// 0~3 랜덤 생성 함수
int RandomNum(vector <int>& arr) {
	random_device rd;
	mt19937 engine(rd());

	shuffle(arr.begin(), arr.end(), engine);

	return arr[0];
}


// 방문하려는 점이 범위를 벗어나지 않는지 체크하는 함수
template <typename T>
bool isValid(int x, int y, vector <T>& vc) {

	return (x > -1 && y > -1 && x < vc.size() && y < vc[0].size());
}

// 방향에 따라 특정 Hex값을 Return;
int ReturnHexNum(int dir) {
	// 위쪽, 오른쪽, 아래쪽, 왼쪽 순서

	if (dir == 0) {
		return 0x1000;
	}
	else if (dir == 1) {
		return 0x0100;
	}
	else if (dir == 2) {
		return 0x0010;
	}
	else {
		return 0x0001;
	}
}

void backtracking(vector <vector <cell>>& vc, int startX, int startY) {
	stack <pair <int, int>> stk;
	pair <int, int> tmp;
	stk.push({ startX, startY });

	int nowX = startX, nowY = startY;

	vc[startX][startY].visited = true;

	while (!stk.empty()) {
		// 방문하지 않은 방향의 번호를 저장하는 벡터
		vector <int> notVisitedList;

		for (int z = 0; z < vc.size(); z++) {
			for (int x = 0; x < vc[0].size(); x++) {
				if (nowX == z && nowY == x) {
					cout << "X";
				}
				else
					cout << "O";
			}
			cout << endl;
		}

		cout << endl;

		for (int i = 0; i < 4; i++) {
			int nextX = nowX + dx[i];
			int nextY = nowY + dy[i];

			if (isValid(nextX, nextY, vc)) {
				if (vc[nextX][nextY].visited == false) {
					notVisitedList.push_back(i);
				}
			}
		}

		// ★ 여기에도 visited를 false만 들어가야 하는 것 같음 -> 아닌데? 위에서 처리 했는뎅..

		// 주변에 이동할 공간이 한 개라도 있을 경우.
		if (notVisitedList.size() > 0) {
			// 랜덤 한 곳으로 이동할 방향을 저장하는 변수
			int notVisitedDir = RandomNum(notVisitedList);
			// 이동 전에 스택에 푸시해줌
			int nextX = nowX + dx[notVisitedDir], nextY = nowY + dy[notVisitedDir];
			stk.push({ nextX, nextY });
			// 다음 방문지 visited를 true로 변경
			vc[nextX][nextY].visited = true;

			// 현재 셀의 벽과 다음 셀의 벽을 부순다. (길 확보)
			vc[nowX][nowY].entrance = ReturnHexNum(notVisitedDir);
			vc[nextX][nextY].entrance = ReturnHexNum((notVisitedDir + 2) % 4);

			// 이동 방향 별로 벽을 허물 것임

			nowX += dx[notVisitedDir];
			nowY += dy[notVisitedDir];
		}
		// 상하좌우 이동할 공간이 없을 경우
		else {
			// 뒤로 이동함
			tmp = stk.top(); stk.pop();
			nowX = tmp.first, nowY = tmp.second;
		}

		// 상하좌우 notVisited 처리 후 배열 Clear
		notVisitedList.clear();
	}
}


int main() {
	vector <cell> vc2(2);
	vector <vector <cell>> vc(2, vc2);

	backtracking(vc, 0, 0);

}
