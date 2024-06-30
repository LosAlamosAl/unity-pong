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

Unity tool's "Save Project" menu item doesn't update scene files.
Closing project window brings up "Save" dialog. That save **does**
update the scene files (and Git will now see them as modified).

### Using Mouse Scroll Wheel
Gave up on using the mouse scrollwheel to move the paddle. There
are too many platoform dependent issues, especially when an
"acceleration" is set on the scroll. In this case many events
can be sent per frame. MacOS is probably different than Windows
and would necessitate platform specific _speed_ variables.

It's possible to use absolute mouse positions and move the
paddle towards that position. I may try that at some point.

This **did not** work well (putting everything in `Update`
worked fine):
```cs
    private void Update()
    {
        _inputVeritcal = Input.mouseScrollDelta.y;
        if (_inputVeritcal != 0) Debug.Log($"scroll delta = {_inputVeritcal}");
    }

    private void FixedUpdate() {
        if (_inputVeritcal != 0) {
            _rb.AddForce(new Vector2(0f, _inputVeritcal * _speed));
        }
    }
```