namespace Carfox.Extensions
{
    public class UploadImages
    {
        public static string UploadFile(IFormFile file, string folderName)
        {
            // 1. Get Located FolderPath
            //string folderPath = "E:\\Courses\\.NET\05 ASP.NET Core MVC\\Session 05\\MVC S05\\Demo.PL Solution\\Demo.PL\\wwwroot\\Files\\"; // m4 altf haga
            //string folderPath = Directory.GetCurrentDirectory() + "\\wwwroot\\Files\\" + folderName; // m4 altf haga

            string folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", folderName);

            // 2. Get File Name and Make it Unique
            // 3l4an ykon unique m7tag generate guid
            //string fileName = file.FileName; // esm el file nfso m4 el se8a bt3to
            string fileName = $"{Guid.NewGuid()}{file.FileName}"; // UNIQUE

            // 3. Get File Path
            string filePath = Path.Combine(folderPath, fileName);

            // 4. Save File as Streams
            // Streams : Data Per Time [Live Videos in Facebook]

            using var fs = new FileStream(filePath, FileMode.Create);
            // CreateNew => lw geh y3ml file w l2a esm el file mwgod abl kda hydrb exception

            file.CopyTo(fs);

            return fileName;
        }

    }
}
