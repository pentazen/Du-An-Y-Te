using Blog_Guitar.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models = Service.EntityModel;

namespace Service
{
    public class BenhAn_TapTin_Service : IService<Models.BenhAn_TapTin>
    {
        public bool CapNhatBenhAnTapTin(Models.BenhAn benhAn,List<int> dsTapTin)
        {
            try
            {
                List<Models.BenhAn_TapTin> DSBenhAnTapTin = new List<Models.BenhAn_TapTin>();
                foreach (var item in dsTapTin)
                {
                    Models.BenhAn_TapTin BenhAnTapTIn = new EntityModel.BenhAn_TapTin();
                    BenhAnTapTIn.id_BenhAn = benhAn.id;
                    BenhAnTapTIn.id_TapTin = item;
                    BenhAnTapTIn.TrangThai = true;
                    DSBenhAnTapTin.Add(BenhAnTapTIn);
                }

                DbContext.BenhAn_TapTin.AddRange(DSBenhAnTapTin);
                return (DbContext.SaveChanges() > 0);
            }
            catch(Exception ex)
            {
                return false;
            }
        }

        public bool XoaTapTinBenhAn(int idBenhAn,int idTapTin)
        {
            Models.BenhAn_TapTin BenhAnTapTin = DbContext.BenhAn_TapTin.FirstOrDefault(tv => tv.id_BenhAn == idBenhAn && tv.id_TapTin == idTapTin && tv.TrangThai == true);
            if (BenhAnTapTin != null)
            {
                BenhAnTapTin.TrangThai = false;
                DbContext.BenhAn_TapTin.Attach(BenhAnTapTin);
                DbContext.Entry(BenhAnTapTin).Property(e => e.TrangThai).IsModified = true;

                DbContext.SaveChanges();
                return true;
            }
            return false;
        }
    }
}
