namespace GeoSys.Domain.Helpers
{
    public class ApiResponse
    {

        #region  Private fields
        private string data;
        private bool isSuccess;
        private string reason;
        private int statusCode;
        private object dataObject;
        #endregion

        #region Properties
        public string Data { get => data; set => data = value; }
        public bool IsSuccess { get => isSuccess; set => isSuccess = value; }
        public string Reason { get => reason; set => reason = value; }
        public int StatusCode { get => statusCode; set => statusCode = value; }
        public object DataObject { get => dataObject; set => dataObject = value; }
        #endregion
    }
}