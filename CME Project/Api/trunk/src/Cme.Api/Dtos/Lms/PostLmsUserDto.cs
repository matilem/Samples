namespace Aafp.Cme.Api.Dtos.Lms
{
    public class PostLmsUserDto
    {
        public string AuthName { get; set; }

        public string Module => "aafp_sso";
    }
}