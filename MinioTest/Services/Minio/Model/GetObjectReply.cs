using Minio.DataModel;

namespace MinioTest.Services.Minio.Model
{
    public class GetObjectDto
    {
        public byte[] Bytes { get; set; }
        public string ContentType { get; set; }
    }
}