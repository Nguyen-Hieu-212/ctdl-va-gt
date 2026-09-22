#include <iostream>
#include <string>
using namespace std;

struct SinhVien
{
    int MaSV;
    string HoTen;
    string Lop;
    float DiemTongKet;
    string HanhKiem;
};

struct Node
{
    SinhVien data;
    Node *left;
    Node *right;
};
void KhoiTaoCay(Node *&root)
{
    root = NULL;
}
Node* TaoNode(SinhVien sv)
{
    Node *p = new Node;

    p->data = sv;
    p->left = NULL;
    p->right = NULL;

    return p;
}
void ChenNode(Node *&root, SinhVien sv)
{
    if (root == NULL)
    {
        root = TaoNode(sv);
        return;
    }

    if (sv.MaSV < root->data.MaSV)
    {
        ChenNode(root->left, sv);
    }
    else if (sv.MaSV > root->data.MaSV)
    {
        ChenNode(root->right, sv);
    }
    else
    {
        cout << "Ma sinh vien da ton tai!\n";
    }
}
Node* TimKiem(Node *root, int ma)
{
    if (root == NULL)
    {
        return NULL;
    }

    if (ma == root->data.MaSV)
    {
        return root;
    }

    if (ma < root->data.MaSV)
    {
        return TimKiem(root->left, ma);
    }
    else
    {
        return TimKiem(root->right, ma);
    }
}
SinhVien NhapSinhVien()
{
    SinhVien sv;

    cout << "Ma sinh vien: ";
    cin >> sv.MaSV;
    cin.ignore();

    cout << "Ho ten: ";
    getline(cin, sv.HoTen);

    cout << "Lop: ";
    getline(cin, sv.Lop);

    cout << "Diem tong ket: ";
    cin >> sv.DiemTongKet;
    cin.ignore();

    cout << "Hanh kiem: ";
    getline(cin, sv.HanhKiem);

    return sv;
}
void XuatSinhVien(SinhVien sv)
{
    cout << "Ma SV: " << sv.MaSV << endl;
    cout << "Ho ten: " << sv.HoTen << endl;
    cout << "Lop: " << sv.Lop << endl;
    cout << "Diem tong ket: " << sv.DiemTongKet << endl;
    cout << "Hanh kiem: " << sv.HanhKiem << endl;
}
void DuyetCay(Node *root)
{
    if (root != NULL)
    {
        DuyetCay(root->left);

        XuatSinhVien(root->data);
        cout << "--------------------------\n";

        DuyetCay(root->right);
    }
}
int main()
{
    Node *root;
    KhoiTaoCay(root);

    int n;

    cout << "Nhap so luong sinh vien: ";
    cin >> n;
    for (int i = 0; i < n; i++)
    {
        cout << "\n===== Nhap sinh vien thu " << i + 1 << " =====\n";

        SinhVien sv = NhapSinhVien();

        ChenNode(root, sv);
    }

    // C4 
    cout << "\n========== DANH SACH SINH VIEN ==========\n";
    DuyetCay(root);

    // C5 
    int ma;

    cout << "\nNhap ma sinh vien can tim: ";
    cin >> ma;

    Node *p = TimKiem(root, ma);

    if (p != NULL)
    {
        cout << "\n===== THONG TIN SINH VIEN CAN TIM =====\n";
        XuatSinhVien(p->data);
    }
    else
    {
        cout << "\nKhong co sinh vien co ma " << ma << " trong cay!\n";
    }

    return 0;
} 
