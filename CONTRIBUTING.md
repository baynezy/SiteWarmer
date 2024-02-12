# Contributing

## Pull Requests

After forking the repository please create a pull request before creating the fix. This way we can talk about how the
fix will be implemented. This will greatly increase your chance of your patch getting merged into the code base.

## Commit Template

Please run the following to make sure you commit messages conform to the project
standards.

```bash
git config --local commit.template .gitmessage
```

## Markdown

There are linting rules for the markdown documentation in this project. So
please adhere to them. This can be achieved by installing the node module
`markdownlint-cli`.

```bash
npm install -g markdownlint-cli
```

Then to check your Markdown run.

```bash
markdownlint .
```