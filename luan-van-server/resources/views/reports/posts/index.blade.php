@extends('layouts.master')

@section('content')
    <!-- Blank Start -->
    <div class="container-fluid pt-4 px-4">
        <div class="row bg-secondary rounded justify-content-center mx-0">
            <div class="row text-center">
                <div class="col-12">
                    <div class="bg-secondary rounded p-4 ">
                        <div class="table-responsive">

                            <h3>Duyệt qua các bài viết bị báo cáo</h3>
                            <table class="table">
                                <thead>
                                    <tr>
                                        <th scope="col">ID</th>
                                        <th scope="col">Người báo cáo</th>
                                        <th scope="col">lý do báo cáo</th>
                                        <th scope="col">nội dung báo cáo</th>
                                        <th scope="col">Xem bài viết</th>
                                        <th scope="col">Chi tiết</th>
                                    </tr>
                                </thead>
                                @foreach ($reports as $report)
                                    <tbody>
                                        <tr>
                                            <th class="align-middle" scope="row">{{ $report->id }}</th>
                                            <td class="align-middle">{{ $report->user->name }}</td>
                                            <td class="align-middle w-25" style="white-space: pre-line">{{ $report->reportType->name }}</td>
                                            <td class="align-middle w-25" style="white-space: pre-line">{{ $report->content }}
                                            </td>
                                            <td class="align-middle">
                                                <button type="button" class="btn btn-primary" data-bs-toggle="modal" data-bs-target="#modal{{ $report->id }}">
                                                    Xem bài viết
                                                </button>
                                            </td>
                                            <td class="align-middle">
                                                <a href="#" class="btn btn-info">Duyệt qua</a>
                                            </td>
                                        </tr>
                                    </tbody>
                                @endforeach
                            </table>
                        </div>

                        @foreach ($reports as $report)
                            <div class="modal fade" id="modal{{ $report->id }}" data-bs-backdrop="static" data-bs-keyboard="false" tabindex="-1" aria-labelledby="staticBackdropLabel" aria-hidden="true">
                                <div class="modal-dialog modal-dialog-scrollable">
                                    <div class="modal-content">
                                        <div class="modal-header">
                                            <h1 class="modal-title fs-5 text-light" id="staticBackdropLabel">
                                                Bài viết của {{ $report->user->name }}
                                            </h1>
                                            <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                                        </div>
                                        <div class="modal-body">
                                            @if ($report->model->title != '')
                                                <h5 class="text-dark text-start">
                                                    Tiêu đề: {{ $report->model->title }}
                                                </h5>
                                            @endif
                                            <p class="text-dark text-start">
                                                Nội dung: {{ $report->model->content }}
                                            </p>
                                            <div>
                                                @if ($report->model->image_path != '')
                                                    <img src="{{ $report->model->image_path }}"class="img-fluid" style="max-height: 250px;">
                                                @endif
                                            </div>
                                        </div>
                                        <div class="modal-footer">
                                            <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Thoát ra</button>
                                            {{-- <button type="button" class="btn btn-primary">Understood</button> --}}
                                        </div>
                                    </div>
                                </div>
                            </div>
                        @endforeach

                        @foreach ($commentReports as $commentReport)
                            <br />
                            Báo cáo Comment: {{ $commentReport->model->content }}
                        @endforeach
                    </div>
                </div>
            </div>
        </div>
    </div>
    <!-- Blank End -->
@stop
