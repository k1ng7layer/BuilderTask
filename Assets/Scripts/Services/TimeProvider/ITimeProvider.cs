﻿namespace Services.TimeProvider
{
    public interface ITimeProvider
    {
        float DeltaTime { get; }
    }
}