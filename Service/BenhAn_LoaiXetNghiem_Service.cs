using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models = Service.EntityModel;
namespace Service
{
    public class BenhAn_LoaiXetNghiem_Service : IService
    {
        public List<Models.BenhAn_LoaiXetNghiem> CapNhatLoaiXetNghiemChoBenhAn(Models.BenhAn benhAn, List<Models.BenhAn_LoaiXetNghiem> DanhSachBenhAnLoaiXetNghiem)
        {
            Models.BenhAn benhAnCapNhat = DuAnYTeEntitiesFramework.BenhAns.FirstOrDefault(ba => ba.id == benhAn.id);

            //List<Models.BenhAn_LoaiXetNghiem> KetQua = new List<Models.BenhAn_LoaiXetNghiem>();
            foreach (Models.BenhAn_LoaiXetNghiem BenhAnLoaiXetNghiem in DanhSachBenhAnLoaiXetNghiem)
            {
                BenhAnLoaiXetNghiem.id_BenhAn = benhAnCapNhat.id;
                for (int i = 0; i < benhAnCapNhat.BenhAn_LoaiXetNghiem.Count; i++)
                {
                    Models.BenhAn_LoaiXetNghiem beAn_lxn_hienTai = benhAnCapNhat.BenhAn_LoaiXetNghiem.ToList()[i];
                    var check = CheckLoaiXetNghiemInBenhAnLoaiXetNghiem(benhAnCapNhat, BenhAnLoaiXetNghiem);
                    if (!check)
                    {
                        BenhAnLoaiXetNghiem.TrangThai = true;
                        benhAnCapNhat.BenhAn_LoaiXetNghiem.Add(BenhAnLoaiXetNghiem);
                        break;
                    }
                    if (BenhAnLoaiXetNghiem.id_LoaiXetNghiem == beAn_lxn_hienTai.id_LoaiXetNghiem)
                    {
                        benhAnCapNhat.BenhAn_LoaiXetNghiem.ToList()[i].GiaTri = BenhAnLoaiXetNghiem.GiaTri;
                        benhAnCapNhat.BenhAn_LoaiXetNghiem.ToList()[i].DonVi = BenhAnLoaiXetNghiem.DonVi;
                        benhAnCapNhat.BenhAn_LoaiXetNghiem.ToList()[i].TrangThai = true;
                        break;
                    }

                }
            }
            DuAnYTeEntitiesFramework.SaveChanges();
            return DanhSachBenhAnLoaiXetNghiem;
        }
        public bool CheckLoaiXetNghiemInBenhAnLoaiXetNghiem(Models.BenhAn benhAn, Models.BenhAn_LoaiXetNghiem ba_lxn)
        {
            for (int i = 0; i < benhAn.BenhAn_LoaiXetNghiem.Count; i++)
            {
                Models.BenhAn_LoaiXetNghiem beAn_lxn_hienTai = benhAn.BenhAn_LoaiXetNghiem.ToList()[i];
                if (ba_lxn.id_LoaiXetNghiem == beAn_lxn_hienTai.id_LoaiXetNghiem)
                {
                    return true;
                }

            }
            return false;
        }
        public Models.BenhAn_LoaiXetNghiem LayGiaTriTheoIdBenhAnIdLoaiXetNghiem(int idBenhAn, int idLoaiXetNghiem)
        {
            return DuAnYTeEntitiesFramework.BenhAn_LoaiXetNghiem.AsEnumerable().FirstOrDefault(x => x.id_LoaiXetNghiem == idLoaiXetNghiem && x.id_BenhAn == idBenhAn);
        }
    }
}
