using Nimbus.MessageContracts;

namespace CloudScale.Movies.Messages
{
    public class IsRegisteredRequest : IBusRequest<IsRegisteredRequest, IsRegisteredResponse>
    {
        /// <summary>
        ///     Initializes a new instance of the IsRegisteredRequest class.
        /// </summary>
        /// <param name="name"></param>
        public IsRegisteredRequest(string name)
        {
            Name = name;
        }

        public IsRegisteredRequest()
        {
        }

        public string Name { get; set; }
    }
}