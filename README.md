# Learn Unity by Building Pong

## Initial Setup

Create a new Unity 2D (using built-in rendering pipeline). Make sure it's set up for VScode.

Create a new repo on GitHub with the same name as the Unity project (directory). Don't add any files (e.g. README.md, .gitignore) via the GitHub UI.

In the new Unity project directory, add the standard [Unity .gitignore file](https://github.com/github/gitignore/blob/main/Unity.gitignore).

Prepare for the initial commit:

```sh
git config gpg.format ssh
git config user.signingkey 'ssh-ed25519 your-GitHub-SSH-key email@domain.com'
git config commit.gpgsign true
git config user.name "Your Name"
git config user.email "email@domain.com"
git add -A
git commit -m "Initial commit"
git branch -M main
git remote add origin git@github.com:YourGitHubID/your-repo-name.git
git push -u origin main
```
