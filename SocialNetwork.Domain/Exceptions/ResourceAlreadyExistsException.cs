﻿
namespace SocialNetwork.Domain.Exceptions;

[Serializable]
public class ResourceAlreadyExistsException : Exception
{
    public ResourceAlreadyExistsException()
    {
    }

    public ResourceAlreadyExistsException(string? message) : base(message)
    {
    }

    public ResourceAlreadyExistsException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}