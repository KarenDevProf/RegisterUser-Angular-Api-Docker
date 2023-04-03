namespace RegisterUser.BusinessLayer.Exceptions
{
    public class NotFoundException : BaseException
    {
        public NotFoundException(string errorType) : base(errorType)
        {

        }
    }
}
