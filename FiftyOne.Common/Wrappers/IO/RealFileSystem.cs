/* *********************************************************************
 * This Original Work is copyright of 51 Degrees Mobile Experts Limited.
 * Copyright 2019 51 Degrees Mobile Experts Limited, 5 Charlotte Close,
 * Caversham, Reading, Berkshire, United Kingdom RG4 7BY.
 *
 * This Original Work is licensed under the European Union Public Licence (EUPL) 
 * v.1.2 and is subject to its terms as set out below.
 *
 * If a copy of the EUPL was not distributed with this file, You can obtain
 * one at https://opensource.org/licenses/EUPL-1.2.
 *
 * The 'Compatible Licences' set out in the Appendix to the EUPL (as may be
 * amended by the European Commission) shall be deemed incompatible for
 * the purposes of the Work and the provisions of the compatibility
 * clause in Article 5 of the EUPL shall not apply.
 * 
 * If using the Work as, or as part of, a network application, by 
 * including the attribution notice(s) required under Article 5 of the EUPL
 * in the end user terms of the application under an appropriate heading, 
 * such notice(s) shall fulfill the requirements of that article.
 * ********************************************************************* */

using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace FiftyOne.Common.Wrappers.IO
{
    public class RealFileSystem : IFileSystem
    {
        public IFileWrapper File { get; } = new FileWrapper();
        public IDirectoryWrapper Directory { get; } = new DirectoryWrapper();

        public IFileInfoWrapper GetFileInfo(FileInfo file)
        {
            return new FileInfoWrapper(GetFileInfo, GetDirInfo, file);
        }
        public IFileInfoWrapper GetFileInfo(string fileName)
        {
            return new FileInfoWrapper(GetFileInfo, GetDirInfo, fileName);
        }

        public IDirectoryInfoWrapper GetDirInfo(DirectoryInfo dir)
        {
            return new DirectoryInfoWrapper(GetFileInfo, GetDirInfo, dir);
        }
        public IDirectoryInfoWrapper GetDirInfo(string dirName)
        {
            return new DirectoryInfoWrapper(GetFileInfo, GetDirInfo, dirName);
        }
    }
}