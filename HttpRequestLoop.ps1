while($true)
{
    Invoke-WebRequest -URI https://localhost:7195/api/MemoryLeak/ReadData
    Start-Sleep -Milliseconds 1000
}
