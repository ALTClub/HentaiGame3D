import os, stat, glob

HOOKS='.git/hooks'
PRE=f'{HOOKS}/pre-commit'

if not os.path.exists(HOOKS):
    os.mkdir(HOOKS)

with open(PRE, 'w') as f:
    f.write('#!/bin/env python\n')
    f.write('import subprocess\n')
    f.write(f'subprocess.run(["dotnet", "format", "--verify-no-changes", "{glob.glob("*.sln")[0]}"], check=True)\n')

st = os.stat(PRE)
os.chmod(PRE, st.st_mode | stat.S_IEXEC)
