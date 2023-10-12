@extends('layouts.master')

@section('content')
    <!-- Blank Start -->
    <div class="container-fluid pt-4 px-4">
        <div class="row bg-secondary rounded justify-content-center mx-0">
            <div class="row text-center">
                <div class="col-12">
                    <div class="bg-secondary rounded p-4 ">
                        <div class="row d-inline-flex justify-content-between align-items-center w-100">
                            <h3 class="col-6 text-start">Các lớp học của bạn</h3>
                            <a href="" class="col-2 btn btn-outline-success rounded">+ Tạo lớp học mới</a>
                        </div>

                        <div class="table-responsive ">
                            <table class="table ">
                                <thead>
                                    <tr>
                                        <th scope="col">ID</th>
                                        <th scope="col">Tên lớp</th>
                                        <th scope="col">Miêu tả lớp học</th>
                                        <th scope="col">Chỉnh sửa</th>
                                    </tr>
                                </thead>
                                @foreach ($classrooms as $classroom)
                                    <tbody>
                                        <tr>
                                            <th class="align-middle" scope="row">{{ $classroom->id }}</th>
                                            <td class="align-middle">{{ $classroom->name }}</td>
                                            <td class="align-middle" style="white-space: pre-line">{{ $classroom->description }}</td>
                                            <td class="align-middle">
                                                <a href="{{ route('classrooms.show', ['id' => $classroom->id]) }}" class="btn btn-info">Chỉnh sửa</a>
                                            </td>
                                        </tr>
                                    </tbody>
                                @endforeach
                            </table>
                        </div>
                    </div>

                </div>
            </div>
        </div>
    </div>
    <!-- Blank End -->
@stop
