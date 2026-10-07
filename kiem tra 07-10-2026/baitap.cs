#include <iostream>
#include <iomanip>
#include <string>
#include <limits>
using namespace std;

// Câu 1: Định nghĩa cấu trúc Khách hàng
struct KhachHang {
    int maKH;            // Mã khách hàng
    string tenKH;        // Tên khách hàng
    string soDienThoai;  // Số điện thoại
    double tongTien;     // Tổng tiền thanh toán
};

// Câu 2: Nhập mảng gồm n khách hàng
void nhapDanhSach(KhachHang a[], int n) {
    for (int i = 0; i < n; i++) {
        cout << "\n--- Nhap khach hang thu " << i + 1 << " ---\n";
        cout << "Ma khach hang: ";
        cin >> a[i].maKH;
        cin.ignore(numeric_limits<streamsize>::max(), '\n');
        cout << "Ten khach hang: ";
        getline(cin, a[i].tenKH);
        cout << "So dien thoai: ";
        getline(cin, a[i].soDienThoai);
        cout << "Tong tien thanh toan: ";
        cin >> a[i].tongTien;
    }
}

// In một khách hàng (dùng chung cho xuất và tìm kiếm)
void xuatMotKhachHang(const KhachHang &kh) {
    cout << left << setw(10) << kh.maKH
         << setw(25) << kh.tenKH
         << setw(15) << kh.soDienThoai
         << fixed << setprecision(2) << kh.tongTien << "\n";
}

// Câu 3: Xuất mảng n khách hàng
void xuatDanhSach(const KhachHang a[], int n) {
    cout << "\n" << left << setw(10) << "Ma KH"
         << setw(25) << "Ten KH"
         << setw(15) << "So dien thoai"
         << "Tong tien\n";
    cout << string(65, '-') << "\n";
    for (int i = 0; i < n; i++)
        xuatMotKhachHang(a[i]);
}

// Câu 4: Sắp xếp chèn trực tiếp (Insertion Sort), tăng dần theo tổng tiền
void insertionSort(KhachHang a[], int n) {
    for (int i = 1; i < n; i++) {
        KhachHang key = a[i];
        int j = i - 1;
        while (j >= 0 && a[j].tongTien > key.tongTien) {
            a[j + 1] = a[j];
            j--;
        }
        a[j + 1] = key;
    }
}

// Câu 5: Tìm kiếm nhị phân (chia để trị) - đệ quy
// Trả về vị trí MỘT khách hàng có tongTien == x trong đoạn [left, right], -1 nếu không có
// (mảng phải đã sắp xếp tăng dần theo tổng tiền)
int binarySearch(const KhachHang a[], int left, int right, double x) {
    if (left > right)
        return -1;
    int mid = left + (right - left) / 2;
    if (a[mid].tongTien == x)
        return mid;
    if (a[mid].tongTien > x)
        return binarySearch(a, left, mid - 1, x);
    return binarySearch(a, mid + 1, right, x);
}

// Tìm và hiển thị TẤT CẢ khách hàng có tổng tiền bằng x
// (sau khi nhị phân tìm được 1 vị trí, mở rộng sang trái/phải vì các giá trị bằng nhau nằm liền kề)
void timKhachHangTheoTongTien(const KhachHang a[], int n, double x) {
    int pos = binarySearch(a, 0, n - 1, x);
    if (pos == -1) {
        cout << "\nKhong co khach hang nao co tong tien thanh toan bang "
             << fixed << setprecision(2) << x << "\n";
        return;
    }
    int l = pos, r = pos;
    while (l - 1 >= 0 && a[l - 1].tongTien == x) l--;
    while (r + 1 < n && a[r + 1].tongTien == x) r++;

    cout << "\nCac khach hang co tong tien thanh toan bang "
         << fixed << setprecision(2) << x << ":\n";
    cout << left << setw(10) << "Ma KH"
         << setw(25) << "Ten KH"
         << setw(15) << "So dien thoai"
         << "Tong tien\n";
    cout << string(65, '-') << "\n";
    for (int i = l; i <= r; i++)
        xuatMotKhachHang(a[i]);
}

// Câu 6: Hàm chính
int main() {
    int n;
    do {
        cout << "Nhap so luong khach hang n: ";
        cin >> n;
    } while (n <= 0);

    KhachHang *ds = new KhachHang[n];

    // Nhập và hiển thị
    nhapDanhSach(ds, n);
    cout << "\n=== DANH SACH KHACH HANG VUA NHAP ===";
    xuatDanhSach(ds, n);

    // Sắp xếp tăng dần theo tổng tiền
    insertionSort(ds, n);
    cout << "\n=== DANH SACH SAU KHI SAP XEP TANG DAN THEO TONG TIEN ===";
    xuatDanhSach(ds, n);

    // Tìm kiếm theo X
    double x;
    cout << "\nNhap tong tien can tim X: ";
    cin >> x;
    timKhachHangTheoTongTien(ds, n, x);

    delete[] ds;
    return 0;
}
