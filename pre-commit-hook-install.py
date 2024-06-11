import os, stat, glob, re

HOOKS='.git/hooks'

if not os.path.exists(HOOKS):
    os.mkdir(HOOKS)

FILES = [
    (f'{HOOKS}/pre-commit', f'''
        #!/bin/sh
        dotnet format --verify-no-changes {(glob.glob("*.sln") or ["Game.sln"])[0]}
    '''),
    (f'{HOOKS}/post-commit', f'''
        #!/bin/sh
        git lfs migrate import --include-ref="$(git rev-parse --abbrev-ref HEAD)" --above=500kb
    ''')
]

if __name__ == '__main__':
    for name, content in FILES:
        with open(name, 'w') as f:
            f.write(re.sub(r'^\s+', '', content, flags=re.MULTILINE))
        st = os.stat(name)
        os.chmod(name, st.st_mode | stat.S_IEXEC)
