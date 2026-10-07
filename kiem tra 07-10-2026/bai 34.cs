#include <iostream>
#include <iomanip>
#include <string>
#include <limits>
using namespace std;

// Kiểu ngày/tháng/năm
struct Ngay {
    int ngay, thang, nam;
};

// Câu 1: Định nghĩa cấu trúc Nhân viên
struct NhanVien {
    int maNV;        // Mã nhân viên
    string hoTen;    // Họ tên
    Ngay ngaySinh;   // Ngày sinh (dd/mm/yyyy)
    double luong;    // Lương (đơn vị: triệu đồng)
};

// Câu 2: Nhập mảng gồm n nhân viên
void nhapDanhSach(NhanVien a[], int n) {
    for (int i = 0; i < n; i++) {
        cout << "\n--- Nhap nhan vien thu " << i + 1 << " ---\n";
        cout << "Ma nhan vien: ";
        cin >> a[i].maNV;
        cin.ignore(numeric_limits<streamsize>::max(), '\n');
        cout << "Ho ten: ";
        getline(cin, a[i].hoTen);
        cout << "Ngay sinh (dd/mm/yyyy, vi du 10/10/2000): ";
        char c;
        cin >> a[i].ngaySinh.ngay >> c
            >> a[i].ngaySinh.thang >> c
            >> a[i].ngaySinh.nam;
        cout << "Luong (trieu dong): ";
        cin >> a[i].luong;
    }
}

// In một nhân viên (dùng chung cho xuất và tìm kiếm)
void xuatMotNhanVien(const NhanVien &nv) {
    cout << left << setw(10) << nv.maNV
         << setw(25) << nv.hoTen;

    // In ngày sinh dạng dd/mm/yyyy
    cout << right << setfill('0')
         << setw(2) << nv.ngaySinh.ngay << "/"
         << setw(2) << nv.ngaySinh.thang << "/"
         << setw(4) << nv.ngaySinh.nam
         << setfill(' ') << left;
    cout << "   " << fixed << setprecision(2) << nv.luong << "\n";
}

void inTieuDe() {
    cout << left << setw(10) << "Ma NV"
         << setw(25) << "Ho ten"
         << setw(13) << "Ngay sinh"
         << "Luong (trieu)\n";
    cout << string(62, '-') << "\n";
}

// Câu 3: Xuất mảng n nhân viên
void xuatDanhSach(const NhanVien a[], int n) {
    cout << "\n";
    inTieuDe();
    for (int i = 0; i < n; i++)
        xuatMotNhanVien(a[i]);
}

// Câu 4: Sắp xếp nổi bọt (Bubble Sort), tăng dần theo lương
void bubbleSort(NhanVien a[], int n) {
    for (int i = 0; i < n - 1; i++) {
        bool daDoiCho = false;
        for (int j = 0; j < n - 1 - i; j++) {
            if (a[j].luong > a[j + 1].luong) {
                NhanVien tmp = a[j];
                a[j] = a[j + 1];
                a[j + 1] = tmp;
                daDoiCho = true;
            }
        }
        if (!daDoiCho) break;  // Mảng đã có thứ tự, dừng sớm
    }
}

// Câu 5: Tìm kiếm nhị phân (chia để trị) - đệ quy
// Trả về vị trí MỘT nhân viên có luong == x trong đoạn [left, right], -1 nếu không có
// (mảng phải đã sắp xếp tăng dần theo lương)
int binarySearch(const NhanVien a[], int left, int right, double x) {
    if (left > right)
        return -1;
    int mid = left + (right - left) / 2;
    if (a[mid].luong == x)
        return mid;
    if (a[mid].luong > x)
        return binarySearch(a, left, mid - 1, x);
    return binarySearch(a, mid + 1, right, x);
}

// Tìm và hiển thị TẤT CẢ nhân viên có lương bằng x
// (sau khi nhị phân tìm được 1 vị trí, mở rộng sang trái/phải vì các giá trị bằng nhau nằm liền kề)
void timNhanVienTheoLuong(const NhanVien a[], int n, double x) {
    int pos = binarySearch(a, 0, n - 1, x);
    if (pos == -1) {
        cout << "\nKhong co nhan vien nao co luong bang "
             << fixed << setprecision(2) << x << " trieu dong\n";
        return;
    }
    int l = pos, r = pos;
    while (l - 1 >= 0 && a[l - 1].luong == x) l--;
    while (r + 1 < n && a[r + 1].luong == x) r++;

    cout << "\nCac nhan vien co luong bang "
         << fixed << setprecision(2) << x << " trieu dong:\n";
    inTieuDe();
    for (int i = l; i <= r; i++)
        xuatMotNhanVien(a[i]);
}

// Câu 6: Hàm chính
int main() {
    int n;
    do {
        cout << "Nhap so luong nhan vien n: ";
        cin >> n;
    } while (n <= 0);

    NhanVien *ds = new NhanVien[n];

    // Nhập và hiển thị
    nhapDanhSach(ds, n);
    cout << "\n=== DANH SACH NHAN VIEN VUA NHAP ===";
    xuatDanhSach(ds, n);

    // Sắp xếp tăng dần theo lương
    bubbleSort(ds, n);
    cout << "\n=== DANH SACH SAU KHI SAP XEP TANG DAN THEO LUONG ===";
    xuatDanhSach(ds, n);

    // Tìm kiếm theo X
    double x;
    cout << "\nNhap muc luong can tim X (trieu dong): ";
    cin >> x;
    timNhanVienTheoLuong(ds, n, x);

    delete[] ds;
    return 0;
}.c
