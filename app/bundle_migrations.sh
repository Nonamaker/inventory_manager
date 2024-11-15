#!/bin/bash
# dotnet ef migrations add MigrationName  <- Run this manually first
dotnet ef migrations bundle --force --self-contained -r linux-x64