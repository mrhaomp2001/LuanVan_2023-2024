@echo off

for /f "tokens=2 delims==" %%a in ('wmic OS Get localdatetime /value') do set "dt=%%a"
set "YY=%dt:~2,2%" & set "YYYY=%dt:~0,4%" & set "MM=%dt:~4,2%" & set "DD=%dt:~6,2%"
set "HH=%dt:~8,2%" & set "Min=%dt:~10,2%" & set "Sec=%dt:~12,2%"

set "fullstamp=%HH%:%Min%:%Sec% %YYYY%/%MM%/%DD%"

echo: >> "readme.md"
echo # %fullstamp% ===== >> "readme.md"
echo: >> "readme.md"
echo ^<details^>^<summary^> ^<h2^>Kế hoạch:^</h2^> ^</summary^> >> "readme.md"
echo: >> "readme.md"
echo ### Kết quả: >> "readme.md"
echo: >> "readme.md"
echo ### Công việc ngày mai: >> "readme.md"
echo: >> "readme.md"
echo ^</details^> >> "readme.md"

start readme.md

