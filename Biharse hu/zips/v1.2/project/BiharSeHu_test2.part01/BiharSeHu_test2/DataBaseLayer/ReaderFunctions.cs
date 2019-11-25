using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLayer;
using System.Data.SqlClient;
using System.Configuration;

namespace DataBaseLayer
{
    public class ReaderFunctions
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["DatabaseContext"].ConnectionString;
        /// <summary>
        /// Fetches LoginInfo Async of the user of id ID
        /// </summary>
        /// <param name="id">AdminId</param>
        /// <returns></returns>
        public async Task<LoginInfo> getLoginInfo(int id)
        {
            LoginInfo Login = null;
            using (SqlConnection con=new SqlConnection(connectionString) )
            {
                using (SqlCommand cmd =new SqlCommand("spGetLoginInfoOfId", con))
                {
                    try
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        SqlParameter idparam = new SqlParameter("@id", System.Data.SqlDbType.Int);
                        idparam.Value = id;
                        cmd.Parameters.Add(idparam);

                        await con.OpenAsync();
                        using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                        {
                            await reader.ReadAsync();
                            Login = new LoginInfo()
                            {
                                AdminId = id,
                                EmailId = reader["emailid"].ToString(),
                                Password = reader["password"].ToString(),
                                Username = reader["username"].ToString()
                            };
                        }
                    }
                    catch (Exception)
                    {

                        throw;
                    }
                }
            }
            return Login;
        }
        /// <summary>
        /// Fetches the Info of user of user id UserId
        /// </summary>
        /// <param name="UserId"> UserID of User</param>
        /// <returns></returns>
        public async Task<UserInfo> getUserInfo(int UserId)
        {
            UserInfo uinfo = null;
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("spGetUserInfoOfId", con))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    SqlParameter idparam = new SqlParameter("@id", System.Data.SqlDbType.Int);
                    idparam.Value = UserId;
                    cmd.Parameters.Add(idparam);

                    await con.OpenAsync();
                    using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                    {
                        await reader.ReadAsync();
                        uinfo = new UserInfo()
                        {
                            userid = UserId,
                            AdminId =Convert.ToInt32(reader["AdminId"]),
                            CreatedOn = Convert.ToDateTime(reader["CreatedOn"]),
                            DateOfBirth = Convert.ToDateTime(reader["DateOfBirth"]),
                            DisplayName = reader["DisplayName"].ToString(),
                            isAdmin = Convert.ToInt32(reader["isAdmin"]) == 1 ? true : false,
                            isAuthor = Convert.ToInt32(reader["isAuthor"]) == 1 ? true : false,
                            Name=reader["Name"].ToString(),
                            NoOfPosts= Convert.ToInt32(reader["NoOfPosts"]),
                            NoOfTags= Convert.ToInt32(reader["NoOfTags"])
                        };
                    }
                    con.Close();
                }
            }
            return uinfo;
        }
        /// <summary>
        /// Fetches PostData of PostId postId
        /// </summary>
        /// <param name="postId">Id of desired post</param>
        /// <returns></returns>
        public async Task<PostMetaData> getPostMetadata(int postId)
        {
            PostMetaData postdata = null;
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("spGetPostMetadataOfId", con))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    SqlParameter idparam = new SqlParameter("@id", System.Data.SqlDbType.Int);
                    idparam.Value = postId;
                    cmd.Parameters.Add(idparam);

                    await con.OpenAsync();
                    using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                    {
                        await reader.ReadAsync();
                        postdata = new PostMetaData()
                        {
                            PostId=postId,
                            AssociatedAuthorID=Convert.ToInt32(reader["authorId"]),
                            CreatedOn=Convert.ToDateTime(reader["CreatedOn"]),
                            HasPics=(Convert.ToInt32(reader["hasPics"])==1)?true:false,
                            HasTags = (Convert.ToInt32(reader["hasTags"]) == 1) ? true : false,
                            IsSponsoredPost = (Convert.ToInt32(reader["isSponsoredPost"]) == 1) ? true : false,
                            NoOfLikes = Convert.ToInt32(reader["noOfLikes"]),
                            NoOfPics = Convert.ToInt32(reader["noOfPics"]),
                            NoOfTags = Convert.ToInt32(reader["noOfTags"]),
                            NoOfViews = Convert.ToInt32(reader["noOfViews"])
                        };
                    }
                    con.Close();
                }
            }
            return postdata;
        }
        /// <summary>
        /// Fetches PostContent of postContentId id
        /// </summary>
        /// <param name="postContentId">id of desired Post Content</param>
        /// <returns></returns>
        public async Task<PostContent> getPostContent(int postContentId)
        {
            PostContent postContent = null;
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("spGetPostContentOfId", con))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    SqlParameter idparam = new SqlParameter("@id", System.Data.SqlDbType.Int);
                    idparam.Value = postContentId;
                    cmd.Parameters.Add(idparam);

                    await con.OpenAsync();
                    using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                    {
                        await reader.ReadAsync();
                        postContent = new PostContent()
                        {
                            Content=reader["Content"].ToString(),
                            Heading = reader["Heading"].ToString(),
                            PostContentId=postContentId,
                            PostId=Convert.ToInt32(reader["postId"])
                        };
                    }
                    con.Close();
                }
            }
            return postContent;
        }
        /// <summary>
        /// Fetches metadata of Picture of metadata Id picId
        /// </summary>
        /// <param name="picId">id of desired data</param>
        /// <returns>PictureMetadata object</returns>
        public async Task<PictureMetadata> getPictureMetadata(int picId)
        {
            PictureMetadata pictureMetadata = null;
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("spGetPictureMetadataOfId", con))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    SqlParameter idparam = new SqlParameter("@id", System.Data.SqlDbType.Int);
                    idparam.Value = picId;
                    cmd.Parameters.Add(idparam);

                    await con.OpenAsync();
                    using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                    {
                        await reader.ReadAsync();
                        pictureMetadata = new PictureMetadata()
                        {
                            PicId = picId,
                            AssociatedPostId = Convert.ToInt32(reader["AssociatedPostId"]),
                            FileSize = Convert.ToInt32(reader["fileSize"]),
                            Height = Convert.ToInt32(reader["Height"]),
                            Width = Convert.ToInt32(reader["Width"]),
                            PicturePriority = Convert.ToInt32(reader["picturePriority"]),
                            IsPostMainPicture = (Convert.ToInt32(reader["isPostMainPicture"]) == 1) ? true : false,
                            IsPostBackground = (Convert.ToInt32(reader["isPostBackground"]) == 1) ? true : false,
                            Format = reader["Format"].ToString(),
                            CreatedOn = Convert.ToDateTime(reader["CreatedOn"])
                        };
                    }
                    con.Close();
                }
            }
            return pictureMetadata;
        }
        /// <summary>
        /// Fetches PictureContent of desired id
        /// </summary>
        /// <param name="picContentId">id of desired picture</param>
        /// <returns>PictureContent</returns>
        public async Task<PictureContent> getPictureContent(int picContentId)
        {
            PictureContent pictureContent = null;
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("spGetPictureContentOfId", con))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    SqlParameter idparam = new SqlParameter("@id", System.Data.SqlDbType.Int);
                    idparam.Value = picContentId;
                    cmd.Parameters.Add(idparam);

                    await con.OpenAsync();
                    using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                    {
                        await reader.ReadAsync();
                        pictureContent = new PictureContent()
                        {
                            PicContentId = picContentId,
                            PicId = Convert.ToInt32(reader["PicId"]),
                            Caption = reader["Caption"].ToString(),
                            Path = reader["Path"].ToString(),
                            PictureFileName = reader["PictureFileName"].ToString(),
                            PictureThumbnailPath = reader["PicThumbnailPath"].ToString(),
                            TakenBy = reader["takenBy"].ToString()
                        };
                    }
                    con.Close();
                }
            }
            return pictureContent;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="tagId"></param>
        /// <returns></returns>
        public async Task<Tags> getTag(int tagId)
        {
            Tags tag = null;
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("spGetTagOfId", con))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    SqlParameter idparam = new SqlParameter("@id", System.Data.SqlDbType.Int);
                    idparam.Value = tagId;
                    cmd.Parameters.Add(idparam);

                    await con.OpenAsync();
                    using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                    {
                        await reader.ReadAsync();
                        tag = new Tags()
                        {
                            TagId = tagId,
                            TagCounter = Convert.ToInt32(reader["tagCounter"]),
                            IsNewTag = (Convert.ToInt32(reader["isNewTag"]) == 1) ? true : false,
                            Content = reader["Content"].ToString(),
                            CreatedOn = Convert.ToDateTime(reader["CreatedOn"])
                        };
                    }
                    con.Close();
                }
            }
            return tag;
        }
        /// <summary>
        /// Fetches a list of posts associated to a tag of id tagId
        /// </summary>
        /// <param name="tagId"> desired tag id</param>
        /// <returns>List of Post</returns>
        public async Task<List<Post>> getPostsAssociatedToTag(int tagId)
        {
            List<Post> posts = new List<Post>();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("spGetPostsAssociatedToTag", con))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    SqlParameter idparam = new SqlParameter("@id", System.Data.SqlDbType.Int);
                    idparam.Value = tagId;
                    cmd.Parameters.Add(idparam);

                    await con.OpenAsync();
                    using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            PostContent_Tags PT = new PostContent_Tags()
                            {
                                PostContentId=Convert.ToInt32(reader["postId"]),
                                TagId = tagId
                            };
                            PostContent postC = await getPostContent(PT.PostContentId);
                            posts.Add(new Post()
                            {
                                postContent = postC,
                                postMetaData = await getPostMetadata(postC.PostId)
                            });
                        }
                    }
                    con.Close();
                }
            }
            return posts;
        }

        /// <summary>
        /// Fetches a list of Users associated to a tag of id tagId
        /// </summary>
        /// <param name="tagId">desired tag id</param>
        /// <returns></returns>
        public async Task<List<User>> getUsersAssociatedToTag(int tagId)
        {
            List<User> users = new List<User>();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("spGetUsersAssociatedToTag", con))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    SqlParameter idparam = new SqlParameter("@id", System.Data.SqlDbType.Int);
                    idparam.Value = tagId;
                    cmd.Parameters.Add(idparam);

                    await con.OpenAsync();
                    using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            UserInfo_Tags UT = new UserInfo_Tags()
                            {
                                TagId = tagId,
                                UserId = Convert.ToInt32(reader["UserId"])
                            };
                            UserInfo Uinfo = await getUserInfo(UT.UserId);
                            LoginInfo linfo = await getLoginInfo(Uinfo.AdminId);
                            User user = new User();
                            user.userInfo = Uinfo;
                            user.changeLoginInfo(linfo);
                            users.Add(user);
                        }
                    }
                    con.Close();
                }
            }
            return users;
        }
        /// <summary>
        /// Fetches the user associated to a post of id postid
        /// </summary>
        /// <param name="postId">Desired Post's Id</param>
        /// <returns>User Object</returns>
        public async Task<User> getUserAssociatedToPost(int postId)
        {
            PostMetaData postdata = await getPostMetadata(postId);
            User user = new User();
            user.userInfo = await getUserInfo(postdata.AssociatedAuthorID);
            user.changeLoginInfo(await getLoginInfo(user.userInfo.AdminId));
            return user;
        }
        /// <summary>
        /// Fetches pictureContent according to the pic Id
        /// </summary>
        /// <param name="picId">Picture Id</param>
        /// <returns>PictureContent Object</returns>
        public async Task<PictureContent> getPictureContentAccToPicId(int picId)
        {
            PictureContent pictureContent = null;
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("spGetPictureContentOfIdAccToPicId", con))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    SqlParameter idparam = new SqlParameter("@id", System.Data.SqlDbType.Int);
                    idparam.Value = picId;
                    cmd.Parameters.Add(idparam);

                    await con.OpenAsync();
                    using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                    {
                        await reader.ReadAsync();
                        pictureContent = new PictureContent()
                        {
                            PicContentId = Convert.ToInt32(reader["PicContentId"]),
                            PicId = picId,
                            Caption = reader["Caption"].ToString(),
                            Path = reader["Path"].ToString(),
                            PictureFileName = reader["PictureFileName"].ToString(),
                            PictureThumbnailPath = reader["PicThumbnailPath"].ToString(),
                            TakenBy = reader["takenBy"].ToString()
                        };
                    }
                    con.Close();
                }
            }
            return pictureContent;
        }
        /// <summary>
        /// Fetches a list of Pictures associated to a post
        /// </summary>
        /// <param name="postId"> desired Post id</param>
        /// <returns>List of Pictures</returns>
        public async Task<List<Picture>> getPicturesAssociatedToPost(int postId)
        {
            List<Picture> pics = new List<Picture>();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("spGetPicturesAssociatedToPost", con))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    SqlParameter idparam = new SqlParameter("@id", System.Data.SqlDbType.Int);
                    idparam.Value = postId;
                    cmd.Parameters.Add(idparam);

                    await con.OpenAsync();
                    using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            PictureMetadata picMetadata = new PictureMetadata()
                            {
                                PicId = Convert.ToInt32(reader["PicId"]),
                                AssociatedPostId = postId,
                                FileSize = Convert.ToInt32(reader["fileSize"]),
                                Height = Convert.ToInt32(reader["Height"]),
                                Width = Convert.ToInt32(reader["Width"]),
                                PicturePriority = Convert.ToInt32(reader["picturePriority"]),
                                IsPostMainPicture = (Convert.ToInt32(reader["isPostMainPicture"]) == 1) ? true : false,
                                IsPostBackground = (Convert.ToInt32(reader["isPostBackground"]) == 1) ? true : false,
                                Format = reader["Format"].ToString(),
                                CreatedOn = Convert.ToDateTime(reader["CreatedOn"])
                            };
                            PictureContent picContent = await getPictureContentAccToPicId(picMetadata.PicId);
                            pics.Add(new Picture()
                            {
                                pictureContent = picContent,
                                pictureMetadata = picMetadata
                            });
                        }
                    }
                    con.Close();
                }
            }
            return pics;
        }

        public async Task<UserInfo> getUserInfoByUsername(string Username)
        {
            UserInfo uinfo = null;
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("spGetUserInfoOfUsername", con))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    SqlParameter pUsername = new SqlParameter("@username", System.Data.SqlDbType.NVarChar);
                    pUsername.Value = Username;
                    cmd.Parameters.Add(pUsername);

                    await con.OpenAsync();
                    using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                    {
                        await reader.ReadAsync();
                        uinfo = new UserInfo()
                        {
                            userid = Convert.ToInt32(reader["userId"]),
                            AdminId = Convert.ToInt32(reader["AdminId"]),
                            CreatedOn = Convert.ToDateTime(reader["CreatedOn"]),
                            DateOfBirth = Convert.ToDateTime(reader["DateOfBirth"]),
                            DisplayName = reader["DisplayName"].ToString(),
                            isAdmin = Convert.ToInt32(reader["isAdmin"]) == 1 ? true : false,
                            isAuthor = Convert.ToInt32(reader["isAuthor"]) == 1 ? true : false,
                            Name = reader["Name"].ToString(),
                            NoOfPosts = Convert.ToInt32(reader["NoOfPosts"]),
                            NoOfTags = Convert.ToInt32(reader["NoOfTags"])
                        };
                    }
                    con.Close();
                }
            }
            return uinfo;
        }

        public async Task<User> getUserOfUsername(string username)
        {
            User user = new User();
            user.userInfo = await getUserInfoByUsername(username);
            user.changeLoginInfo(await getLoginInfo(user.userInfo.AdminId));
            return user;
        }
    }
}
