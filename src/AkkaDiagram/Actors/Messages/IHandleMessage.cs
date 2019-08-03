namespace AkkaDiagram.Actors.Messages
{
    /// <summary>
    ///
    /// </summary>
    public interface IHandleMessage
    {
        /// <summary>
        ///
        /// </summary>
        /// <returns>returns Boolean if it could be handled by the Handler</returns>
        bool Handle();

        /// <summary>
        /// Gets the Tag
        /// </summary>
        string Tag { get; }
    }
}