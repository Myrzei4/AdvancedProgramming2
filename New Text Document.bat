@echo off
setlocal
set "root_dir=%cd%"
echo Cleaning project: %root_dir%

for /d /r %%d in (bin,obj) do (
    if exist "%%d" (
        echo Deleting directory: %%d
        rd /s /q "%%d"
    )
)

echo Project cleaned.
endlocal
pause
