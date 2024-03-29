# SiteWarmer

Allows you to hit your sites after a deployment to make sure they are running.

## Build Status

<table>
    <tr>
        <th>master</th>
		<td><a href="https://github.com/baynezy/SiteWarmer/actions/workflows/branch-master.yml"><img src="https://github.com/baynezy/SiteWarmer/actions/workflows/branch-master.yml/badge.svg?branch=master" alt="master" title="master" /></a></td>
    </tr>
    <tr>
        <th>develop</th>
		<td><a href="https://github.com/baynezy/SiteWarmer/actions/workflows/branch-develop.yml"><img src="https://github.com/baynezy/SiteWarmer/actions/workflows/branch-develop.yml/badge.svg?branch=develop" alt="develop" title="develop" /></a></td>
    </tr>
</table>

## Documentation
Fully navigable documentation available on [GitHub Pages](http://baynezy.github.io/SiteWarmer/)

## Installing via NuGet

[![NuGet version](https://badge.fury.io/nu/SiteWarmer.Core.svg)](http://badge.fury.io/nu/SiteWarmer.Core)

    Install-Package SiteWarmer.Core

## Usage

See full Wiki https://github.com/baynezy/SiteWarmer/wiki

### Config file

You need to specify the URLs you want to use in a text file. They need to be one per line to work
e.g.

    http://www.google.com/
    http://www.github.com/

### As a Console Application

Once you build the solution there will be a bin\Release\output\SiteWarmer.exe file created. This is a combination of the console application and all its dependencies. So you only need the exe file.

To use the application you just need to run the .exe from the command line with one parameter that is the relative or absolute path to the configuration file.
e.g.

    \> SiteWarmer.exe -i Config.txt
	
## Contributing

### Pull Requests

After forking the repository please create a pull request before creating the fix. This way we can talk about how the fix will be implemented. This will greatly increase your chance of your patch getting merged into the code base.

## License
This project is licensed under [Apache License 2.0](http://www.apache.org/licenses/LICENSE-2.0).