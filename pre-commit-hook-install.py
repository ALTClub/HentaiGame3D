#!/bin/env python

import os, stat

PRE='.git/hooks/pre-commit'
os.mkdir('.git/hooks')

with open(PRE, 'w') as f:
    f.write('#!/bin/env python\n')
    f.write('import subprocess\n')
    f.write('subprocess.run(["dotnet", "format", "--verify-no-changes", "Game.sln"], check=True)\n')

st = os.stat(PRE)
os.chmod(PRE, st.st_mode | stat.S_IEXEC)
