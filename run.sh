#!/bin/sh

dotnet build && ./plugcpy.sh && cd ./PhoneApp && dotnet run

