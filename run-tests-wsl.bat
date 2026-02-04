@echo off
REM Run IntegrationTests via WSL with Podman
REM This wrapper script invokes the WSL bash script

setlocal

set WSL_PATH=wsl

REM Get current directory in Windows path format
set "CURRENT_DIR=%cd%"

REM Convert Windows path to WSL path format for command line
set "WSL_DIR=%CURRENT_DIR%"
set "WSL_DIR=%WSL_DIR:\=/%"
set "WSL_DIR=%WSL_DIR:C:=/mnt/c%"
set "WSL_DIR=%WSL_DIR:D:=/mnt/d%"
set "WSL_DIR=%WSL_DIR:E:=/mnt/e%"

REM Change to WSL path and run the script
%WSL_PATH% bash -c "cd '%WSL_DIR%' && bash run-tests-wsl.sh %*"
