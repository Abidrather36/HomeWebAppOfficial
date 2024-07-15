namespace HomeWebApp.Domain.Enums
{
    public enum StatusCode
    {
        
        //Success
        OK = 200,
        Created = 201,
        Accepted = 202,
        NoContent = 204,
        PartialContent = 206,

        // Client Error
        BadRequest = 400,
        Unauthorized = 401,
        Forbidden = 403,
        NotFound = 404,
        MethodNotAllowed = 405,
        NotAcceptable = 406,
        Conflict = 409,
        Gone = 410,
        LengthRequired = 411,
        PreconditionFailed = 412,
        PayloadTooLarge = 413,
        URITooLong = 414,
        UnsupportedMediaType = 415,
        RangeNotSatisfiable = 416,
        ExpectationFailed = 417,
        UnprocessableEntity = 422,
        TooManyRequests = 429,


        // Server Errors
        InternalServerError = 500,
        NotImplemented = 501,
        BadGateway = 502,
        ServiceUnavailable = 503,
        GatewayTimeout = 504,


        // Redirect Errors
        MovedPermanently = 301,
        Found = 302,
        SeeOther = 303,
        TemporaryRedirect = 307,
        PermanentRedirect = 308,

        // Other Codes
        Continue = 100,
        SwitchingProtocols = 101,
        Processing = 102,
        EarlyHints = 103,
        NonAuthoritativeInformation = 203,
        ResetContent = 205,
        MultiStatus = 207,
        AlreadyReported = 208,
        IMUsed = 226,
        Ambiguous = 300,
        MultipleChoices = 300,
        Moved = 301,
        Redirect = 302,
        RedirectMethod = 303,
        NotModified = 304,
        UseProxy = 305,
        Unused = 306,
        RedirectKeepVerb = 307,
        PaymentRequired = 402,
        ProxyAuthenticationRequired = 407,
        RequestTimeout = 408,
        RequestEntityTooLarge = 413,
        RequestUriTooLong = 414,
        RequestedRangeNotSatisfiable = 416,
        MisdirectedRequest = 421,
        Locked = 423,
        FailedDependency = 424,
        UpgradeRequired = 426,
        PreconditionRequired = 428,
        RequestHeaderFieldsTooLarge = 431,
        UnavailableForLegalReasons = 451,
        HttpVersionNotSupported = 505,
        VariantAlsoNegotiates = 506,
        InsufficientStorage = 507,
        LoopDetected = 508,
        NotExtended = 510,
        NetworkAuthenticationRequired = 511

    }
}

