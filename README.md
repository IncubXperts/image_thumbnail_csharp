<!-- Improved compatibility of back to top link: See: https://github.com/othneildrew/Best-README-Template/pull/73 -->
<a name="readme-top"></a>
<!-- PROJECT SHIELDS -->
<!--
*** I'm using markdown "reference style" links for readability.
*** Reference links are enclosed in brackets [ ] instead of parentheses ( ).
*** See the bottom of this document for the declaration of the reference variables
*** for contributors-url, forks-url, etc. This is an optional, concise syntax you may use.
*** https://www.markdownguide.org/basic-syntax/#reference-style-links
-->
[![Contributors][contributors-shield]][contributors-url]
[![Forks][forks-shield]][forks-url]
[![Stargazers][stars-shield]][stars-url]
[![Issues][issues-shield]][issues-url]
[![MIT License][license-shield]][license-url]
[![LinkedIn][linkedin-shield]][linkedin-url]

<!-- PROJECT LOGO -->
<br />
<div align="center">
  <a href="https://github.com/IncubXperts/image_thumbnail_csharp">
    <img src="images/logo.png" alt="Logo" width="80" height="80">
  </a>

<h3 align="center">Image Thumbnail Generator using C#</h3>
  <p align="center">
    This is an effort by [IncubXperts](https://incubxperts.com) to provide commonly used source code snippets as open source code to community. Feel free to use it in your projects, suggest improvements, report bugs to improve the code for community.  Feel free to contribute. 
    <br />
    Source code example to generate a thumbnail from an image.  
    <br />
    <a href="https://github.com/IncubXperts/image_thumbnail_csharp/issues">Report Bug</a>
   ·
    <a href="https://github.com/IncubXperts/image_thumbnail_csharp/issues">Request Feature</a>
   ·
    <a href="https://github.com/IncubXperts/code-ideas-requests/issues">Request new source code</a>

  </p>
</div>



<!-- TABLE OF CONTENTS -->
<details>
  <summary>Table of Contents</summary>
  <ol>
    <li>
      <a href="#about-the-project">About The Project</a>
      <ul>
        <li><a href="#built-with">Built With</a></li>
      </ul>
    </li>
    <li>
      <a href="#getting-started">Getting Started</a>
      <ul>
        <li><a href="#prerequisites">Prerequisites</a></li>
        <li><a href="#installation">Installation</a></li>
      </ul>
    </li>
    <li><a href="#usage">Usage</a></li>
    <li><a href="#Contributing">Contributing</a></li>
    <li><a href="#license">License</a></li>
    <li><a href="#contact">Contact</a></li>
    <li><a href="#acknowledgments">Acknowledgments</a></li>
  </ol>
</details>



<!-- ABOUT THE PROJECT -->
## About The Project

Source code example to generate a thumbnail from an image. 
In our software projects many times, we need to generate image thumbnails. For example, If a user is uploading an image to your web application and displays it on UI. Generally, we show such images in small sizes compared to the actual image size. It's always a better idea to generate a thumbnail to show on UI and when the user clicks on the thumbnail image we should show full-size images only when needed.  This saves bandwidth and makes the application load faster.

This sample source code provides an example to create a  thumbnail from a full sized image. 
Typical use in web application 
The user uploads an image to the application 
Make the image available as a stream 
The given code converts the original image to a thumbnail and returns it as a stream. 
Store the thumbnail image along with the original image for display purposes.

<p align="right">(<a href="#readme-top">back to top</a>)</p>


### Built With

Console app and sample code in C# class.
* [![C#][C Sharp Dotnet 6]][https://dotnet.microsoft.com/en-us/download/dotnet/6.0]

<p align="right">(<a href="#readme-top">back to top</a>)</p>

<!-- GETTING STARTED -->
## Getting Started

This is an example of how you may give instructions on setting up your project locally.
To get a local copy up and running follow these simple example steps.

### Prerequisites

This is an example of how to list things you need to use the software and how to install them.
* Dotnet 6 
* Works on Linux as well as windows. 

### Installation

1. Understand the code and use in your project as fit. 

<p align="right">(<a href="#readme-top">back to top</a>)</p>



<!-- USAGE EXAMPLES -->
## Usage

```
        static void GenerateThumbnailFromFile(string InputImageFile, string OutputThumbnailImageFile)
        {
            // Open source file as file stream.
            using FileStream inputFileStream = new(InputImageFile, FileMode.Open);
            // Call image thumbnail generator to generate thumnail 
            using (var thumbnailMemStream = ImageThumbnailGenerator.GenerateThumbnail(inputFileStream, 200, 200))
            {
                var outputFileStream = new FileStream(OutputThumbnailImageFile, FileMode.Create, FileAccess.Write);
                thumbnailMemStream.CopyTo(outputFileStream);
                outputFileStream.Dispose();
                thumbnailMemStream.Dispose();
            }
            // Close open streams. 
            inputFileStream.Dispose();
        }

```

<p align="right">(<a href="#readme-top">back to top</a>)</p>



<!-- CONTRIBUTING -->
## Contributing

Contributions are what make the open source community such an amazing place to learn, inspire, and create. Any contributions you make are **greatly appreciated**.

If you have a suggestion that would make this better, please fork the repo and create a pull request. You can also simply open an issue with the tag "enhancement".
Don't forget to give the project a star! Thanks again!

1. Fork the Project
2. Create your Feature Branch (`git checkout -b feature/AmazingFeature`)
3. Commit your Changes (`git commit -m 'Add some AmazingFeature'`)
4. Push to the Branch (`git push origin feature/AmazingFeature`)
5. Open a Pull Request

<p align="right">(<a href="#readme-top">back to top</a>)</p>


<!-- LICENSE -->
## License

Distributed under the GPL License. See `LICENSE.txt` for more information.

<p align="right">(<a href="#readme-top">back to top</a>)</p>


<!-- CONTACT -->
## Contact

IncubXperts - [@incubxperts](https://twitter.com/incubxperts) - contact@incubxperts.com

Project Link: [https://github.com/IncubXperts/image_thumbnail_csharp](https://github.com/IncubXperts/image_thumbnail_csharp)

<p align="right">(<a href="#readme-top">back to top</a>)</p>


<!-- ACKNOWLEDGMENTS -->
## Acknowledgments

* [Developer - Rahul Bagal](https://bagal.in)

<p align="right">(<a href="#readme-top">back to top</a>)</p>

<!-- MARKDOWN LINKS & IMAGES -->
<!-- https://www.markdownguide.org/basic-syntax/#reference-style-links -->
[contributors-shield]: https://img.shields.io/github/contributors/IncubXperts/image_thumbnail_csharp.svg?style=for-the-badge
[contributors-url]: https://github.com/IncubXperts/image_thumbnail_csharp/graphs/contributors
[forks-shield]: https://img.shields.io/github/forks/IncubXperts/image_thumbnail_csharp.svg?style=for-the-badge
[forks-url]: https://github.com/IncubXperts/image_thumbnail_csharp/network/members
[stars-shield]: https://img.shields.io/github/stars/IncubXperts/image_thumbnail_csharp.svg?style=for-the-badge
[stars-url]: https://github.com/IncubXperts/image_thumbnail_csharp/stargazers
[issues-shield]: https://img.shields.io/github/issues/IncubXperts/image_thumbnail_csharp.svg?style=for-the-badge
[issues-url]: https://github.com/IncubXperts/image_thumbnail_csharp/issues
[license-shield]: https://img.shields.io/github/license/IncubXperts/image_thumbnail_csharp.svg?style=for-the-badge
[license-url]: https://github.com/IncubXperts/image_thumbnail_csharp/blob/main/LICENSE
[linkedin-shield]: https://img.shields.io/badge/-LinkedIn-black.svg?style=for-the-badge&logo=linkedin&colorB=555
[linkedin-url]: https://www.linkedin.com/company/incubxperts
[Next.js]: https://img.shields.io/badge/next.js-000000?style=for-the-badge&logo=nextdotjs&logoColor=white
[Next-url]: https://nextjs.org/
[React.js]: https://img.shields.io/badge/React-20232A?style=for-the-badge&logo=react&logoColor=61DAFB
[React-url]: https://reactjs.org/
[Vue.js]: https://img.shields.io/badge/Vue.js-35495E?style=for-the-badge&logo=vuedotjs&logoColor=4FC08D
[Vue-url]: https://vuejs.org/
[Angular.io]: https://img.shields.io/badge/Angular-DD0031?style=for-the-badge&logo=angular&logoColor=white
[Angular-url]: https://angular.io/
[Svelte.dev]: https://img.shields.io/badge/Svelte-4A4A55?style=for-the-badge&logo=svelte&logoColor=FF3E00
[Svelte-url]: https://svelte.dev/
[Laravel.com]: https://img.shields.io/badge/Laravel-FF2D20?style=for-the-badge&logo=laravel&logoColor=white
[Laravel-url]: https://laravel.com
[Bootstrap.com]: https://img.shields.io/badge/Bootstrap-563D7C?style=for-the-badge&logo=bootstrap&logoColor=white
[Bootstrap-url]: https://getbootstrap.com
[JQuery.com]: https://img.shields.io/badge/jQuery-0769AD?style=for-the-badge&logo=jquery&logoColor=white
[JQuery-url]: https://jquery.com 