import os, stat, glob, re, subprocess

def install_csharpier():
    if subprocess.run(['dotnet-csharpier', '-h'], capture_output=True).returncode == 0:
        return

    subprocess.run(['dotnet', 'tool', 'install', '--global', 'csharpier'], stderr=subprocess.PIPE, stdout=subprocess.PIPE)

HOOKS='.git/hooks'

if not os.path.exists(HOOKS):
    os.mkdir(HOOKS)

SOLUTION = glob.glob('*.sln')[0]

FILES = [
    (f'{HOOKS}/pre-commit', f'''
        #!/bin/sh
        set -e

        dotnet-csharpier --check Assets

        dotnet format style {SOLUTION}
        dotnet format analyzers {SOLUTION}
    '''),
    (f'{HOOKS}/post-commit', f'''
        #!/bin/sh
        git lfs migrate import --include-ref="$(git rev-parse --abbrev-ref HEAD)" --above=500kb
    ''')
]

if __name__ == '__main__':
    install_csharpier()
    for name, content in FILES:
        with open(name, 'w') as f:
            f.write(re.sub(r'^\s+', '', content, flags=re.MULTILINE))
        st = os.stat(name)
        os.chmod(name, st.st_mode | stat.S_IEXEC)
