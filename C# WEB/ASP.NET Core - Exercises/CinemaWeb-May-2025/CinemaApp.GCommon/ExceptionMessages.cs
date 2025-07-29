namespace CinemaApp.GCommon
{
    
    public static class ExceptionMessages
    {
        public const string InterfaceNotFoundMessage = "Interface {0} not found for class {1}. " +
                                                      "Please ensure that the interface is defined and follows the naming convention 'I{ClassName}'.";
        public const string RepositoryNotFoundMessage = "Repository {0} not found. " +
                                                        "Please ensure that the repository is defined and follows the naming convention '{ClassName}Repository'.";
        public const string ServiceNotFoundMessage = "Service {0} not found. " +
                                                     "Please ensure that the service is defined and follows the naming convention '{ClassName}Service'.";
    }
}
