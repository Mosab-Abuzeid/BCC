// Generated by the protocol buffer compiler.  DO NOT EDIT!
// source: Protos/MasterBankerService.proto
#region Designer generated code

using System;
using System.Threading;
using System.Threading.Tasks;
using grpc = global::Grpc.Core;

namespace Noqoush.AdFalcon.Banker.Common {
  public static partial class MasterBankerService
  {
    static readonly string __ServiceName = "MasterBankerService";

    static readonly grpc::Marshaller<global::Noqoush.AdFalcon.Banker.Common.SyncAllRequest> __Marshaller_SyncAllRequest = grpc::Marshallers.Create((arg) => global::Google.Protobuf.MessageExtensions.ToByteArray(arg), global::Noqoush.AdFalcon.Banker.Common.SyncAllRequest.Parser.ParseFrom);
    static readonly grpc::Marshaller<global::Noqoush.AdFalcon.Banker.Common.SyncAllResponse> __Marshaller_SyncAllResponse = grpc::Marshallers.Create((arg) => global::Google.Protobuf.MessageExtensions.ToByteArray(arg), global::Noqoush.AdFalcon.Banker.Common.SyncAllResponse.Parser.ParseFrom);
    static readonly grpc::Marshaller<global::Noqoush.AdFalcon.Banker.Common.CreateOrUpdateFundAccountRequest> __Marshaller_CreateOrUpdateFundAccountRequest = grpc::Marshallers.Create((arg) => global::Google.Protobuf.MessageExtensions.ToByteArray(arg), global::Noqoush.AdFalcon.Banker.Common.CreateOrUpdateFundAccountRequest.Parser.ParseFrom);
    static readonly grpc::Marshaller<global::Noqoush.AdFalcon.Banker.Common.EmptyResponse> __Marshaller_EmptyResponse = grpc::Marshallers.Create((arg) => global::Google.Protobuf.MessageExtensions.ToByteArray(arg), global::Noqoush.AdFalcon.Banker.Common.EmptyResponse.Parser.ParseFrom);
    static readonly grpc::Marshaller<global::Noqoush.AdFalcon.Banker.Common.CreateOrUpdateBudgetAccountRequest> __Marshaller_CreateOrUpdateBudgetAccountRequest = grpc::Marshallers.Create((arg) => global::Google.Protobuf.MessageExtensions.ToByteArray(arg), global::Noqoush.AdFalcon.Banker.Common.CreateOrUpdateBudgetAccountRequest.Parser.ParseFrom);
    static readonly grpc::Marshaller<global::Noqoush.AdFalcon.Banker.Common.ChangeFundRequest> __Marshaller_ChangeFundRequest = grpc::Marshallers.Create((arg) => global::Google.Protobuf.MessageExtensions.ToByteArray(arg), global::Noqoush.AdFalcon.Banker.Common.ChangeFundRequest.Parser.ParseFrom);
    static readonly grpc::Marshaller<global::Noqoush.AdFalcon.Banker.Common.CloseAccountRequest> __Marshaller_CloseAccountRequest = grpc::Marshallers.Create((arg) => global::Google.Protobuf.MessageExtensions.ToByteArray(arg), global::Noqoush.AdFalcon.Banker.Common.CloseAccountRequest.Parser.ParseFrom);
    static readonly grpc::Marshaller<global::Noqoush.AdFalcon.Banker.Common.GetFundBalanceRequest> __Marshaller_GetFundBalanceRequest = grpc::Marshallers.Create((arg) => global::Google.Protobuf.MessageExtensions.ToByteArray(arg), global::Noqoush.AdFalcon.Banker.Common.GetFundBalanceRequest.Parser.ParseFrom);
    static readonly grpc::Marshaller<global::Noqoush.AdFalcon.Banker.Common.GetFundBalanceResponse> __Marshaller_GetFundBalanceResponse = grpc::Marshallers.Create((arg) => global::Google.Protobuf.MessageExtensions.ToByteArray(arg), global::Noqoush.AdFalcon.Banker.Common.GetFundBalanceResponse.Parser.ParseFrom);

    static readonly grpc::Method<global::Noqoush.AdFalcon.Banker.Common.SyncAllRequest, global::Noqoush.AdFalcon.Banker.Common.SyncAllResponse> __Method_SyncAll = new grpc::Method<global::Noqoush.AdFalcon.Banker.Common.SyncAllRequest, global::Noqoush.AdFalcon.Banker.Common.SyncAllResponse>(
        grpc::MethodType.Unary,
        __ServiceName,
        "SyncAll",
        __Marshaller_SyncAllRequest,
        __Marshaller_SyncAllResponse);

    static readonly grpc::Method<global::Noqoush.AdFalcon.Banker.Common.CreateOrUpdateFundAccountRequest, global::Noqoush.AdFalcon.Banker.Common.EmptyResponse> __Method_CreateOrUpdateFundAccount = new grpc::Method<global::Noqoush.AdFalcon.Banker.Common.CreateOrUpdateFundAccountRequest, global::Noqoush.AdFalcon.Banker.Common.EmptyResponse>(
        grpc::MethodType.Unary,
        __ServiceName,
        "CreateOrUpdateFundAccount",
        __Marshaller_CreateOrUpdateFundAccountRequest,
        __Marshaller_EmptyResponse);

    static readonly grpc::Method<global::Noqoush.AdFalcon.Banker.Common.CreateOrUpdateBudgetAccountRequest, global::Noqoush.AdFalcon.Banker.Common.EmptyResponse> __Method_CreateOrUpdateBudgetAccount = new grpc::Method<global::Noqoush.AdFalcon.Banker.Common.CreateOrUpdateBudgetAccountRequest, global::Noqoush.AdFalcon.Banker.Common.EmptyResponse>(
        grpc::MethodType.Unary,
        __ServiceName,
        "CreateOrUpdateBudgetAccount",
        __Marshaller_CreateOrUpdateBudgetAccountRequest,
        __Marshaller_EmptyResponse);

    static readonly grpc::Method<global::Noqoush.AdFalcon.Banker.Common.ChangeFundRequest, global::Noqoush.AdFalcon.Banker.Common.EmptyResponse> __Method_ChangeFund = new grpc::Method<global::Noqoush.AdFalcon.Banker.Common.ChangeFundRequest, global::Noqoush.AdFalcon.Banker.Common.EmptyResponse>(
        grpc::MethodType.Unary,
        __ServiceName,
        "ChangeFund",
        __Marshaller_ChangeFundRequest,
        __Marshaller_EmptyResponse);

    static readonly grpc::Method<global::Noqoush.AdFalcon.Banker.Common.CloseAccountRequest, global::Noqoush.AdFalcon.Banker.Common.EmptyResponse> __Method_CloseAccount = new grpc::Method<global::Noqoush.AdFalcon.Banker.Common.CloseAccountRequest, global::Noqoush.AdFalcon.Banker.Common.EmptyResponse>(
        grpc::MethodType.Unary,
        __ServiceName,
        "CloseAccount",
        __Marshaller_CloseAccountRequest,
        __Marshaller_EmptyResponse);

    static readonly grpc::Method<global::Noqoush.AdFalcon.Banker.Common.GetFundBalanceRequest, global::Noqoush.AdFalcon.Banker.Common.GetFundBalanceResponse> __Method_GetFundBalance = new grpc::Method<global::Noqoush.AdFalcon.Banker.Common.GetFundBalanceRequest, global::Noqoush.AdFalcon.Banker.Common.GetFundBalanceResponse>(
        grpc::MethodType.Unary,
        __ServiceName,
        "GetFundBalance",
        __Marshaller_GetFundBalanceRequest,
        __Marshaller_GetFundBalanceResponse);

    /// <summary>Service descriptor</summary>
    public static global::Google.Protobuf.Reflection.ServiceDescriptor Descriptor
    {
      get { return global::Noqoush.AdFalcon.Banker.Common.MasterBankerServiceReflection.Descriptor.Services[0]; }
    }

    /// <summary>Base class for server-side implementations of MasterBankerService</summary>
    public abstract partial class MasterBankerServiceBase
    {
      public virtual global::System.Threading.Tasks.Task<global::Noqoush.AdFalcon.Banker.Common.SyncAllResponse> SyncAll(global::Noqoush.AdFalcon.Banker.Common.SyncAllRequest request, grpc::ServerCallContext context)
      {
        throw new grpc::RpcException(new grpc::Status(grpc::StatusCode.Unimplemented, ""));
      }

      public virtual global::System.Threading.Tasks.Task<global::Noqoush.AdFalcon.Banker.Common.EmptyResponse> CreateOrUpdateFundAccount(global::Noqoush.AdFalcon.Banker.Common.CreateOrUpdateFundAccountRequest request, grpc::ServerCallContext context)
      {
        throw new grpc::RpcException(new grpc::Status(grpc::StatusCode.Unimplemented, ""));
      }

      public virtual global::System.Threading.Tasks.Task<global::Noqoush.AdFalcon.Banker.Common.EmptyResponse> CreateOrUpdateBudgetAccount(global::Noqoush.AdFalcon.Banker.Common.CreateOrUpdateBudgetAccountRequest request, grpc::ServerCallContext context)
      {
        throw new grpc::RpcException(new grpc::Status(grpc::StatusCode.Unimplemented, ""));
      }

      public virtual global::System.Threading.Tasks.Task<global::Noqoush.AdFalcon.Banker.Common.EmptyResponse> ChangeFund(global::Noqoush.AdFalcon.Banker.Common.ChangeFundRequest request, grpc::ServerCallContext context)
      {
        throw new grpc::RpcException(new grpc::Status(grpc::StatusCode.Unimplemented, ""));
      }

      public virtual global::System.Threading.Tasks.Task<global::Noqoush.AdFalcon.Banker.Common.EmptyResponse> CloseAccount(global::Noqoush.AdFalcon.Banker.Common.CloseAccountRequest request, grpc::ServerCallContext context)
      {
        throw new grpc::RpcException(new grpc::Status(grpc::StatusCode.Unimplemented, ""));
      }

      public virtual global::System.Threading.Tasks.Task<global::Noqoush.AdFalcon.Banker.Common.GetFundBalanceResponse> GetFundBalance(global::Noqoush.AdFalcon.Banker.Common.GetFundBalanceRequest request, grpc::ServerCallContext context)
      {
        throw new grpc::RpcException(new grpc::Status(grpc::StatusCode.Unimplemented, ""));
      }

    }

    /// <summary>Client for MasterBankerService</summary>
    public partial class MasterBankerServiceClient : grpc::ClientBase<MasterBankerServiceClient>
    {
      /// <summary>Creates a new client for MasterBankerService</summary>
      /// <param name="channel">The channel to use to make remote calls.</param>
      public MasterBankerServiceClient(grpc::Channel channel) : base(channel)
      {
      }
      /// <summary>Creates a new client for MasterBankerService that uses a custom <c>CallInvoker</c>.</summary>
      /// <param name="callInvoker">The callInvoker to use to make remote calls.</param>
      public MasterBankerServiceClient(grpc::CallInvoker callInvoker) : base(callInvoker)
      {
      }
      /// <summary>Protected parameterless constructor to allow creation of test doubles.</summary>
      protected MasterBankerServiceClient() : base()
      {
      }
      /// <summary>Protected constructor to allow creation of configured clients.</summary>
      /// <param name="configuration">The client configuration.</param>
      protected MasterBankerServiceClient(ClientBaseConfiguration configuration) : base(configuration)
      {
      }

      public virtual global::Noqoush.AdFalcon.Banker.Common.SyncAllResponse SyncAll(global::Noqoush.AdFalcon.Banker.Common.SyncAllRequest request, grpc::Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default(CancellationToken))
      {
        return SyncAll(request, new grpc::CallOptions(headers, deadline, cancellationToken));
      }
      public virtual global::Noqoush.AdFalcon.Banker.Common.SyncAllResponse SyncAll(global::Noqoush.AdFalcon.Banker.Common.SyncAllRequest request, grpc::CallOptions options)
      {
        return CallInvoker.BlockingUnaryCall(__Method_SyncAll, null, options, request);
      }
      public virtual grpc::AsyncUnaryCall<global::Noqoush.AdFalcon.Banker.Common.SyncAllResponse> SyncAllAsync(global::Noqoush.AdFalcon.Banker.Common.SyncAllRequest request, grpc::Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default(CancellationToken))
      {
        return SyncAllAsync(request, new grpc::CallOptions(headers, deadline, cancellationToken));
      }
      public virtual grpc::AsyncUnaryCall<global::Noqoush.AdFalcon.Banker.Common.SyncAllResponse> SyncAllAsync(global::Noqoush.AdFalcon.Banker.Common.SyncAllRequest request, grpc::CallOptions options)
      {
        return CallInvoker.AsyncUnaryCall(__Method_SyncAll, null, options, request);
      }
      public virtual global::Noqoush.AdFalcon.Banker.Common.EmptyResponse CreateOrUpdateFundAccount(global::Noqoush.AdFalcon.Banker.Common.CreateOrUpdateFundAccountRequest request, grpc::Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default(CancellationToken))
      {
        return CreateOrUpdateFundAccount(request, new grpc::CallOptions(headers, deadline, cancellationToken));
      }
      public virtual global::Noqoush.AdFalcon.Banker.Common.EmptyResponse CreateOrUpdateFundAccount(global::Noqoush.AdFalcon.Banker.Common.CreateOrUpdateFundAccountRequest request, grpc::CallOptions options)
      {
        return CallInvoker.BlockingUnaryCall(__Method_CreateOrUpdateFundAccount, null, options, request);
      }
      public virtual grpc::AsyncUnaryCall<global::Noqoush.AdFalcon.Banker.Common.EmptyResponse> CreateOrUpdateFundAccountAsync(global::Noqoush.AdFalcon.Banker.Common.CreateOrUpdateFundAccountRequest request, grpc::Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default(CancellationToken))
      {
        return CreateOrUpdateFundAccountAsync(request, new grpc::CallOptions(headers, deadline, cancellationToken));
      }
      public virtual grpc::AsyncUnaryCall<global::Noqoush.AdFalcon.Banker.Common.EmptyResponse> CreateOrUpdateFundAccountAsync(global::Noqoush.AdFalcon.Banker.Common.CreateOrUpdateFundAccountRequest request, grpc::CallOptions options)
      {
        return CallInvoker.AsyncUnaryCall(__Method_CreateOrUpdateFundAccount, null, options, request);
      }
      public virtual global::Noqoush.AdFalcon.Banker.Common.EmptyResponse CreateOrUpdateBudgetAccount(global::Noqoush.AdFalcon.Banker.Common.CreateOrUpdateBudgetAccountRequest request, grpc::Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default(CancellationToken))
      {
        return CreateOrUpdateBudgetAccount(request, new grpc::CallOptions(headers, deadline, cancellationToken));
      }
      public virtual global::Noqoush.AdFalcon.Banker.Common.EmptyResponse CreateOrUpdateBudgetAccount(global::Noqoush.AdFalcon.Banker.Common.CreateOrUpdateBudgetAccountRequest request, grpc::CallOptions options)
      {
        return CallInvoker.BlockingUnaryCall(__Method_CreateOrUpdateBudgetAccount, null, options, request);
      }
      public virtual grpc::AsyncUnaryCall<global::Noqoush.AdFalcon.Banker.Common.EmptyResponse> CreateOrUpdateBudgetAccountAsync(global::Noqoush.AdFalcon.Banker.Common.CreateOrUpdateBudgetAccountRequest request, grpc::Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default(CancellationToken))
      {
        return CreateOrUpdateBudgetAccountAsync(request, new grpc::CallOptions(headers, deadline, cancellationToken));
      }
      public virtual grpc::AsyncUnaryCall<global::Noqoush.AdFalcon.Banker.Common.EmptyResponse> CreateOrUpdateBudgetAccountAsync(global::Noqoush.AdFalcon.Banker.Common.CreateOrUpdateBudgetAccountRequest request, grpc::CallOptions options)
      {
        return CallInvoker.AsyncUnaryCall(__Method_CreateOrUpdateBudgetAccount, null, options, request);
      }
      public virtual global::Noqoush.AdFalcon.Banker.Common.EmptyResponse ChangeFund(global::Noqoush.AdFalcon.Banker.Common.ChangeFundRequest request, grpc::Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default(CancellationToken))
      {
        return ChangeFund(request, new grpc::CallOptions(headers, deadline, cancellationToken));
      }
      public virtual global::Noqoush.AdFalcon.Banker.Common.EmptyResponse ChangeFund(global::Noqoush.AdFalcon.Banker.Common.ChangeFundRequest request, grpc::CallOptions options)
      {
        return CallInvoker.BlockingUnaryCall(__Method_ChangeFund, null, options, request);
      }
      public virtual grpc::AsyncUnaryCall<global::Noqoush.AdFalcon.Banker.Common.EmptyResponse> ChangeFundAsync(global::Noqoush.AdFalcon.Banker.Common.ChangeFundRequest request, grpc::Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default(CancellationToken))
      {
        return ChangeFundAsync(request, new grpc::CallOptions(headers, deadline, cancellationToken));
      }
      public virtual grpc::AsyncUnaryCall<global::Noqoush.AdFalcon.Banker.Common.EmptyResponse> ChangeFundAsync(global::Noqoush.AdFalcon.Banker.Common.ChangeFundRequest request, grpc::CallOptions options)
      {
        return CallInvoker.AsyncUnaryCall(__Method_ChangeFund, null, options, request);
      }
      public virtual global::Noqoush.AdFalcon.Banker.Common.EmptyResponse CloseAccount(global::Noqoush.AdFalcon.Banker.Common.CloseAccountRequest request, grpc::Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default(CancellationToken))
      {
        return CloseAccount(request, new grpc::CallOptions(headers, deadline, cancellationToken));
      }
      public virtual global::Noqoush.AdFalcon.Banker.Common.EmptyResponse CloseAccount(global::Noqoush.AdFalcon.Banker.Common.CloseAccountRequest request, grpc::CallOptions options)
      {
        return CallInvoker.BlockingUnaryCall(__Method_CloseAccount, null, options, request);
      }
      public virtual grpc::AsyncUnaryCall<global::Noqoush.AdFalcon.Banker.Common.EmptyResponse> CloseAccountAsync(global::Noqoush.AdFalcon.Banker.Common.CloseAccountRequest request, grpc::Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default(CancellationToken))
      {
        return CloseAccountAsync(request, new grpc::CallOptions(headers, deadline, cancellationToken));
      }
      public virtual grpc::AsyncUnaryCall<global::Noqoush.AdFalcon.Banker.Common.EmptyResponse> CloseAccountAsync(global::Noqoush.AdFalcon.Banker.Common.CloseAccountRequest request, grpc::CallOptions options)
      {
        return CallInvoker.AsyncUnaryCall(__Method_CloseAccount, null, options, request);
      }
      public virtual global::Noqoush.AdFalcon.Banker.Common.GetFundBalanceResponse GetFundBalance(global::Noqoush.AdFalcon.Banker.Common.GetFundBalanceRequest request, grpc::Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default(CancellationToken))
      {
        return GetFundBalance(request, new grpc::CallOptions(headers, deadline, cancellationToken));
      }
      public virtual global::Noqoush.AdFalcon.Banker.Common.GetFundBalanceResponse GetFundBalance(global::Noqoush.AdFalcon.Banker.Common.GetFundBalanceRequest request, grpc::CallOptions options)
      {
        return CallInvoker.BlockingUnaryCall(__Method_GetFundBalance, null, options, request);
      }
      public virtual grpc::AsyncUnaryCall<global::Noqoush.AdFalcon.Banker.Common.GetFundBalanceResponse> GetFundBalanceAsync(global::Noqoush.AdFalcon.Banker.Common.GetFundBalanceRequest request, grpc::Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default(CancellationToken))
      {
        return GetFundBalanceAsync(request, new grpc::CallOptions(headers, deadline, cancellationToken));
      }
      public virtual grpc::AsyncUnaryCall<global::Noqoush.AdFalcon.Banker.Common.GetFundBalanceResponse> GetFundBalanceAsync(global::Noqoush.AdFalcon.Banker.Common.GetFundBalanceRequest request, grpc::CallOptions options)
      {
        return CallInvoker.AsyncUnaryCall(__Method_GetFundBalance, null, options, request);
      }
      /// <summary>Creates a new instance of client from given <c>ClientBaseConfiguration</c>.</summary>
      protected override MasterBankerServiceClient NewInstance(ClientBaseConfiguration configuration)
      {
        return new MasterBankerServiceClient(configuration);
      }
    }

    /// <summary>Creates service definition that can be registered with a server</summary>
    /// <param name="serviceImpl">An object implementing the server-side handling logic.</param>
    public static grpc::ServerServiceDefinition BindService(MasterBankerServiceBase serviceImpl)
    {
      return grpc::ServerServiceDefinition.CreateBuilder()
          .AddMethod(__Method_SyncAll, serviceImpl.SyncAll)
          .AddMethod(__Method_CreateOrUpdateFundAccount, serviceImpl.CreateOrUpdateFundAccount)
          .AddMethod(__Method_CreateOrUpdateBudgetAccount, serviceImpl.CreateOrUpdateBudgetAccount)
          .AddMethod(__Method_ChangeFund, serviceImpl.ChangeFund)
          .AddMethod(__Method_CloseAccount, serviceImpl.CloseAccount)
          .AddMethod(__Method_GetFundBalance, serviceImpl.GetFundBalance).Build();
    }

  }
}
#endregion
