﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ExternDotnetSDK.Models.Common;
using ExternDotnetSDK.Models.Docflows;
using ExternDotnetSDK.Models.Documents;
using ExternDotnetSDK.Models.Documents.Data;
using NUnit.Framework;
using Refit;
#pragma warning disable 1998

namespace ExternDotnetSDKTests.SwaggerMethodsTests.Tests
{
    [TestFixture]
    internal class DocflowsClientShould : AllTestsShould
    {
        private readonly Guid badId = Guid.NewGuid();
        private DocflowPage page;
        private DocflowPageItem docflow;
        private List<Document> docflowDocuments;
        private Document document;
        private Signature signature;

        [OneTimeSetUp]
        public override async Task SetUp()
        {
            InitializeClient();
            Account = (await Client.AccountClient.GetAccountsAsync(0, 1)).Accounts[0];
            page = await Client.DocflowsClient.GetDocflowsAsync(Account.Id);
            docflow = page.DocflowsPageItem[0];
            docflowDocuments = await Client.DocflowsClient.GetDocumentsAsync(Account.Id, docflow.Id);
            document = docflowDocuments[0];
            signature = (await Client.DocflowsClient.GetDocumentSignaturesAsync(Account.Id, docflow.Id, document.Id))[0];
        }

        [OneTimeTearDown]
        public override async Task TearDown()
        {
        }

        [Test]
        public void FailToGetDocflows_WithBadParameters()
        {
            var badFilter = new DocflowFilter
            {
                Take = 1,
                InnKpp = "wrong innKpp"
            };
            Assert.ThrowsAsync<ApiException>(async () => await Client.DocflowsClient.GetDocflowsAsync(Account.Id, badFilter));
            Assert.ThrowsAsync<ApiException>(async () => await Client.DocflowsClient.GetDocflowsAsync(badId));
        }

        [Test]
        public void FailToGetSingleDocflow_WithBadParameters()
        {
            Assert.ThrowsAsync<ApiException>(async () => await Client.DocflowsClient.GetDocflowAsync(Account.Id, badId));
            Assert.ThrowsAsync<ApiException>(async () => await Client.DocflowsClient.GetDocflowAsync(badId, docflow.Id));
        }

        [Test]
        public void GetSingleDocflow_WithValidParameters()
        {
            Assert.DoesNotThrowAsync(async () => await Client.DocflowsClient.GetDocflowAsync(Account.Id, docflow.Id));
        }

        [Test]
        public void FailToGetDocflowDocuments_WithBadParameters()
        {
            Assert.ThrowsAsync<ApiException>(async () => await Client.DocflowsClient.GetDocumentsAsync(Account.Id, badId));
            Assert.ThrowsAsync<ApiException>(async () => await Client.DocflowsClient.GetDocumentsAsync(badId, docflow.Id));
        }

        [Test]
        public void GetSingleDocument_WithValidParameters()
        {
            Assert.DoesNotThrowAsync(
                async () => await Client.DocflowsClient.GetDocumentAsync(Account.Id, docflow.Id, document.Id));
        }

        [Test]
        public void FailToGetSingleDocument_WithBadParameters()
        {
            Assert.ThrowsAsync<ApiException>(
                async () => await Client.DocflowsClient.GetDocumentAsync(badId, docflow.Id, document.Id));
            Assert.ThrowsAsync<ApiException>(
                async () => await Client.DocflowsClient.GetDocumentAsync(Account.Id, badId, document.Id));
            Assert.ThrowsAsync<ApiException>(
                async () => await Client.DocflowsClient.GetDocumentAsync(Account.Id, docflow.Id, badId));
        }

        [Test]
        public void GetDocumentDescription_WithValidParameters()
        {
            Assert.DoesNotThrowAsync(
                async () => await Client.DocflowsClient.GetDocumentDescriptionAsync(Account.Id, docflow.Id, document.Id));
        }

        [Test]
        public void GetDocumentEncryptedContent_WhenItExists()
        {
            Assert.DoesNotThrowAsync(
                async () => await Client.DocflowsClient.GetEncryptedDocumentContentAsync(Account.Id, docflow.Id, document.Id));
        }

        [Test]
        public void FailToGetNotExistingEncryptedContent()
        {
            var documentId = docflowDocuments[4].Id;
            Assert.ThrowsAsync<ApiException>(
                async () => await Client.DocflowsClient.GetEncryptedDocumentContentAsync(Account.Id, docflow.Id, documentId));
        }

        [Test]
        public void GetDocumentDecryptedContent_WhenItExists()
        {
            var documentId = docflowDocuments[4].Id;
            Assert.DoesNotThrowAsync(
                async () => await Client.DocflowsClient.GetDecryptedDocumentContentAsync(Account.Id, docflow.Id, documentId));
        }

        [Test]
        public void FailToGetNotExistingDecryptedContent()
        {
            var documentId = docflowDocuments[2].Id;
            Assert.ThrowsAsync<ApiException>(
                async () => await Client.DocflowsClient.GetEncryptedDocumentContentAsync(Account.Id, docflow.Id, documentId));
        }

        [Test]
        public void FailToGetSignatures_WithBadParameters()
        {
            Assert.ThrowsAsync<ApiException>(
                async () => await Client.DocflowsClient.GetDocumentSignaturesAsync(badId, docflow.Id, document.Id));
            Assert.ThrowsAsync<ApiException>(
                async () => await Client.DocflowsClient.GetDocumentSignaturesAsync(Account.Id, badId, document.Id));
            Assert.ThrowsAsync<ApiException>(
                async () => await Client.DocflowsClient.GetDocumentSignaturesAsync(Account.Id, docflow.Id, badId));
        }

        [Test]
        public void GetSingleSignature_WithValidParameters()
        {
            Assert.DoesNotThrowAsync(
                async () => await Client.DocflowsClient.GetSignatureAsync(Account.Id, docflow.Id, document.Id, signature.Id));
        }

        [Test]
        public void FailToGetSingleSignature_WithBadParameters()
        {
            Assert.ThrowsAsync<ApiException>(
                async () => await Client.DocflowsClient.GetSignatureAsync(badId, docflow.Id, document.Id, signature.Id));
            Assert.ThrowsAsync<ApiException>(
                async () => await Client.DocflowsClient.GetSignatureAsync(Account.Id, badId, document.Id, signature.Id));
            Assert.ThrowsAsync<ApiException>(
                async () => await Client.DocflowsClient.GetSignatureAsync(Account.Id, docflow.Id, badId, signature.Id));
            Assert.ThrowsAsync<ApiException>(
                async () => await Client.DocflowsClient.GetSignatureAsync(Account.Id, docflow.Id, document.Id, badId));
        }

        [Test]
        public void FailToGetSignatureContent_WithBadParameters()
        {
            Assert.ThrowsAsync<ApiException>(
                async () => await Client.DocflowsClient.GetSignatureContentAsync(badId, docflow.Id, document.Id, signature.Id));
            Assert.ThrowsAsync<ApiException>(
                async () => await Client.DocflowsClient.GetSignatureContentAsync(Account.Id, badId, document.Id, signature.Id));
            Assert.ThrowsAsync<ApiException>(
                async () => await Client.DocflowsClient.GetSignatureContentAsync(Account.Id, docflow.Id, badId, signature.Id));
            Assert.ThrowsAsync<ApiException>(
                async () => await Client.DocflowsClient.GetSignatureContentAsync(Account.Id, docflow.Id, document.Id, badId));
        }

        [Test]
        public void GetSignatureContent_WithValidParameters()
        {
            Assert.DoesNotThrowAsync(
                async () => await Client.DocflowsClient.GetSignatureContentAsync(
                    Account.Id,
                    docflow.Id,
                    document.Id,
                    signature.Id));
        }

        [Test]
        public void FailToGetApiTask_WithBadApiTaskId()
        {
            Assert.ThrowsAsync<ApiException>(
                async () => await Client.DocflowsClient.GetApiTaskAsync(Account.Id, docflow.Id, document.Id, badId));
        }

        [Test]
        public void FailToGetDocumentReply_WithBadReplyId()
        {
            Assert.ThrowsAsync<ApiException>(
                async () => await Client.DocflowsClient.GetDocumentReplyAsync(Account.Id, docflow.Id, document.Id, badId));
        }

        [Test]
        public void FailToPrintDocument_WhenDocumentPrintUnsupported()
        {
            Assert.ThrowsAsync<ApiException>(
                async () => await Client.DocflowsClient.PrintDocumentAsync(Account.Id, docflow.Id, document.Id, new byte[] {0}));
        }

        [Test]
        public void FailToDecryptDocumentContent_WithBadCertificate()
        {
            Assert.ThrowsAsync<ApiException>(
                async () => await Client.DocflowsClient.DecryptDocumentContentAsync(
                    Account.Id,
                    docflow.Id,
                    document.Id,
                    new DecryptDocumentRequestData {CertificateBase64 = "bad cert"}));
        }
    }
}