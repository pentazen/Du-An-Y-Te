using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#region Thư viện
using Service.EntityModel;
using System.IO;
using System.Reflection;
#endregion
namespace Service
{
    public class TapTin_Service: IService
    {
        public static string DuongDanLuuTapTin = @"D:\LuuTapTin";
        public object LuuTapTin(Stream FileStream, string fileName, string contentType, int fileLength)
        {
            //string path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            //string path2 = Environment.CurrentDirectory;
            string path = DuongDanLuuTapTin;
            if (!path.EndsWith(@"\")) path += @"\";

            //if (File.Exists(path))
            //{
            //    File.Delete(Path.Combine(path, fileName));
            //}
            string[] fileIn = fileName.Split('.');
            int count = 0;
            while (File.Exists(Path.Combine(path, fileName)))
            {
                fileIn[0] += count.ToString();
                fileName = fileIn[0] + "." + fileIn[1];
                count++;
            }

            using (var fileStream = new FileStream(Path.Combine(path, fileName), FileMode.Create, FileAccess.Write))
            {
                FileStream.CopyTo(fileStream);
            }
            TapTin taptin = new TapTin();
            taptin.TenTapTin = fileName;
            taptin.LoaiTapTin = contentType;
            taptin.KichThuoc = fileLength;
            taptin.NgayTao = DateTime.Now;
            taptin.DuongDan = Path.Combine(path, fileName);
            DuAnYTeEntitiesFramework.TapTins.Add(taptin);
            DuAnYTeEntitiesFramework.SaveChanges();
            return taptin.id;
        }
        public object LuuTapTinTraVeDuongDan(Stream FileStream, string fileName, string contentType, int fileLength)
        {
            //string path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            //string path2 = Environment.CurrentDirectory;
            string path = DuongDanLuuTapTin;
            if (!path.EndsWith(@"\")) path += @"\";

            //if (File.Exists(path))
            //{
            //    File.Delete(Path.Combine(path, fileName));
            //}
            string[] fileIn = fileName.Split('.');
            int count = 0;
            while (File.Exists(Path.Combine(path, fileName)))
            {
                fileIn[0] += count.ToString();
                fileName = fileIn[0] + "." + fileIn[1];
                count++;
            }

            using (var fileStream = new FileStream(Path.Combine(path, fileName), FileMode.Create, FileAccess.Write))
            {
                FileStream.CopyTo(fileStream);
            }
            TapTin taptin = new TapTin();
            taptin.TenTapTin = fileName;
            taptin.LoaiTapTin = contentType;
            taptin.KichThuoc = fileLength;
            taptin.NgayTao = DateTime.Now;
            taptin.DuongDan = Path.Combine(path, fileName);
            DuAnYTeEntitiesFramework.TapTins.Add(taptin);
            DuAnYTeEntitiesFramework.SaveChanges();
            return taptin.DuongDan;
        }

        public TapTin GetById(int id)
        {
           return DuAnYTeEntitiesFramework.TapTins.FirstOrDefault(tt => tt.id == id);
        }

        //public object GetStreamByURL(string URL)
        //{
        //    return base.File
        //}
    }
}
