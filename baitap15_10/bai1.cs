#include <iostream>
#include <fstream>
#include <iomanip>
using namespace std;

const int MAXN = 100;

// Doc ma tran trong so tu file .txt
// Dong dau: so dinh n; tiep theo la n dong, moi dong n so (0 = khong co canh)
bool docMaTran(const char* tenFile, int a[MAXN][MAXN], int &n) {
    ifstream f(tenFile);
    if (!f.is_open()) {
        cout << "Khong mo duoc file " << tenFile << endl;
        return false;
    }
    f >> n;
    if (n <= 0 || n > MAXN) {
        cout << "So dinh khong hop le!" << endl;
        return false;
    }
    for (int i = 0; i < n; i++)
        for (int j = 0; j < n; j++)
            f >> a[i][j];
    f.close();
    return true;
}

// In ma tran trong so ra man hinh (dinh duoc ghi nhan A, B, C, ...)
void inMaTran(int a[MAXN][MAXN], int n) {
    cout << "Ma tran trong so:" << endl;
    cout << setw(4) << " ";
    for (int j = 0; j < n; j++)
        cout << setw(4) << char('A' + j);
    cout << endl;
    for (int i = 0; i < n; i++) {
        cout << setw(4) << char('A' + i);
        for (int j = 0; j < n; j++)
            cout << setw(4) << a[i][j];
        cout << endl;
    }
}

int main() {
    int a[MAXN][MAXN];
    int n;
    if (docMaTran("dothi.txt", a, n))
        inMaTran(a, n);
    return 0;
}
