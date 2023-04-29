﻿using Newtonsoft.Json;

namespace PinPoint.ErrorHandling;

public class ErrorDetails
{
    public int StatusCode { get; set; }
    public string? Message { get; set; }

    public override string ToString()
    {
        return JsonConvert.SerializeObject(this);
    }
}