using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using BusinessLayer;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace DataBaseLayer
{
    public class InsertFunctions
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["DatabaseContext"].ConnectionString;

        public async Task<int?> CreateNewAuthor(LoginInfo lI, UserInfo ui)
        {
            try
            {
                int? returnValue = null;
                User user = new User(lI, ui);
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("spInsertNewUserOutUserId", con))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;

                        SqlParameter pUsername = new SqlParameter("@username", System.Data.SqlDbType.NVarChar);
                        pUsername.Value = user.loginInfo.Username;
                        cmd.Parameters.Add(pUsername);

                        SqlParameter pPassword = new SqlParameter("@password", System.Data.SqlDbType.NVarChar);
                        pPassword.Value = user.loginInfo.Password;
                        cmd.Parameters.Add(pPassword);

                        SqlParameter pEmailId = new SqlParameter("@emailid", System.Data.SqlDbType.NVarChar);
                        pEmailId.Value = user.loginInfo.EmailId;
                        cmd.Parameters.Add(pEmailId);

                        SqlParameter pName = new SqlParameter("@name", System.Data.SqlDbType.NVarChar);
                        pName.Value = user.userInfo.Name;
                        cmd.Parameters.Add(pName);

                        SqlParameter pDisplayName = new SqlParameter("@displayname", System.Data.SqlDbType.NVarChar);
                        pDisplayName.Value = user.userInfo.DisplayName;
                        cmd.Parameters.Add(pDisplayName);

                        SqlParameter pCreatedOn = new SqlParameter("@createdon", System.Data.SqlDbType.Date);
                        pCreatedOn.Value = user.userInfo.CreatedOn;
                        cmd.Parameters.Add(pCreatedOn);

                        SqlParameter pNoOfPosts = new SqlParameter("@noofposts", System.Data.SqlDbType.Int);
                        pNoOfPosts.Value = user.userInfo.NoOfPosts;
                        cmd.Parameters.Add(pNoOfPosts);

                        SqlParameter pNoOfTags = new SqlParameter("@nooftags", System.Data.SqlDbType.Int);
                        pNoOfTags.Value = user.userInfo.NoOfTags;
                        cmd.Parameters.Add(pNoOfTags);

                        SqlParameter pDateOfBirth = new SqlParameter("@dateofbirth", System.Data.SqlDbType.Date);
                        pDateOfBirth.Value = user.userInfo.DateOfBirth;
                        cmd.Parameters.Add(pDateOfBirth);

                        SqlParameter pisAuthor = new SqlParameter("@isauthor", System.Data.SqlDbType.Bit);
                        pisAuthor.Value = (user.userInfo.isAuthor) ? 1 : 0;
                        cmd.Parameters.Add(pisAuthor);

                        SqlParameter pisAdmin = new SqlParameter("@isadmin", System.Data.SqlDbType.Bit);
                        pisAdmin.Value = (user.userInfo.isAdmin) ? 1 : 0;
                        cmd.Parameters.Add(pisAdmin);

                        SqlParameter pOutUserId = new SqlParameter("@userid", System.Data.SqlDbType.Int);
                        pOutUserId.Direction = System.Data.ParameterDirection.Output;
                        cmd.Parameters.Add(pOutUserId);

                        if (await cmd.ExecuteNonQueryAsync() == 1)
                        {
                            returnValue = Convert.ToInt32(pOutUserId.Value);
                        }
                        return returnValue;
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<int?> CreateNewPost(PostContent postc,PostMetaData postmd)
        {
            try
            {
                int? returnValue = null;
                Post post = new Post(postc, postmd);
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("spInsertNewPostOutPostContentId", con))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;

                        SqlParameter pHeading = new SqlParameter("@Heading", System.Data.SqlDbType.NVarChar);
                        pHeading.Value = post.postContent.Heading;
                        cmd.Parameters.Add(pHeading);

                        SqlParameter pContent = new SqlParameter("@Content", System.Data.SqlDbType.NVarChar);
                        pContent.Value = post.postContent.Content;
                        cmd.Parameters.Add(pContent);

                        SqlParameter pCreatedOn = new SqlParameter("@createdon", System.Data.SqlDbType.Date);
                        pCreatedOn.Value = post.postMetaData.CreatedOn;
                        cmd.Parameters.Add(pCreatedOn);

                        SqlParameter pnoOfViews = new SqlParameter("@noofViews", System.Data.SqlDbType.Int);
                        pnoOfViews.Value = post.postMetaData.NoOfViews;
                        cmd.Parameters.Add(pnoOfViews);

                        SqlParameter pnoOfLikes = new SqlParameter("@noofLikes", System.Data.SqlDbType.Int);
                        pnoOfLikes.Value = post.postMetaData.NoOfLikes;
                        cmd.Parameters.Add(pnoOfLikes);

                        SqlParameter pnoOfPics = new SqlParameter("@noofpics", System.Data.SqlDbType.Int);
                        pnoOfPics.Value = post.postMetaData.NoOfPics;
                        cmd.Parameters.Add(pnoOfPics);

                        SqlParameter pnoOfTags = new SqlParameter("@nooftags", System.Data.SqlDbType.Int);
                        pnoOfTags.Value = post.postMetaData.NoOfTags;
                        cmd.Parameters.Add(pnoOfTags);
                        
                        SqlParameter phasPics = new SqlParameter("@haspics", System.Data.SqlDbType.Bit);
                        phasPics.Value = post.postMetaData.HasPics?1:0;
                        cmd.Parameters.Add(phasPics);

                        SqlParameter phasTags = new SqlParameter("@hastags", System.Data.SqlDbType.Bit);
                        phasTags.Value = post.postMetaData.HasTags?1:0;
                        cmd.Parameters.Add(phasTags);

                        SqlParameter pIsSponsoredPost = new SqlParameter("@issponsoredpost", System.Data.SqlDbType.Bit);
                        pIsSponsoredPost.Value = post.postMetaData.IsSponsoredPost?1:0;
                        cmd.Parameters.Add(pIsSponsoredPost);

                        SqlParameter pAuthorId = new SqlParameter("@associatedAuthorid", System.Data.SqlDbType.Int);
                        pAuthorId.Value = post.postMetaData.AssociatedAuthorID;
                        cmd.Parameters.Add(pAuthorId);

                        SqlParameter pOutUserId = new SqlParameter("@postcontentid", System.Data.SqlDbType.Int);
                        pOutUserId.Direction = System.Data.ParameterDirection.Output;
                        cmd.Parameters.Add(pOutUserId);

                        if (await cmd.ExecuteNonQueryAsync() == 1)
                        {
                            returnValue = Convert.ToInt32(pOutUserId.Value);
                        }
                        return returnValue;
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<int?> CreateNewTag(Post associatedPost,User associatedUser,Tags tag )
        {
            try
            {
                int? returnValue = null;
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("", con))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;

                        SqlParameter pContent = new SqlParameter("", System.Data.SqlDbType.NVarChar);
                        pContent.Value = tag.Content;
                        cmd.Parameters.Add(pContent);

                        SqlParameter pCreatedOn = new SqlParameter("", System.Data.SqlDbType.Date);
                        pCreatedOn.Value = tag.CreatedOn;
                        cmd.Parameters.Add(pCreatedOn);

                        SqlParameter ptagCounter = new SqlParameter("", System.Data.SqlDbType.Int);
                        ptagCounter.Value = tag.TagCounter;
                        cmd.Parameters.Add(ptagCounter);

                        SqlParameter pisNewTag = new SqlParameter("", System.Data.SqlDbType.Bit);
                        pisNewTag.Value = (tag.IsNewTag) ? 1 : 0;
                        cmd.Parameters.Add(pisNewTag);

                        SqlParameter pAssociatedUserId = new SqlParameter("", System.Data.SqlDbType.Int);
                        pAssociatedUserId.Value = associatedUser.userInfo.userid;
                        cmd.Parameters.Add(pAssociatedUserId);

                        SqlParameter pAssociatedTagId = new SqlParameter("", System.Data.SqlDbType.Int);
                        pAssociatedTagId.Value = associatedPost.postContent.PostId;
                        cmd.Parameters.Add(pAssociatedTagId);

                        SqlParameter pOutUserId = new SqlParameter("", System.Data.SqlDbType.Int);
                        pOutUserId.Direction = System.Data.ParameterDirection.Output;
                        cmd.Parameters.Add(pOutUserId);

                        if (await cmd.ExecuteNonQueryAsync() == 1)
                        {
                            returnValue = Convert.ToInt32(pOutUserId.Value);
                        }
                        return returnValue;

                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
