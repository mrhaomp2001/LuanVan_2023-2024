@extends('layouts.master')

@section('content')
    <!-- Blank Start -->
    <div class="container-fluid pt-4 px-4">
        <div class="row bg-secondary rounded justify-content-center mx-0">
            <div class="row text-center">
                <div class="col-12">
                    <div class="bg-secondary rounded p-4 ">
                        <h2>Thêm lớp học mới</h2>
                        <div class="bg-secondary rounded p-4 ">
                            <form action="{{ route('moderator.classrooms.store') }}" method="POST">
                                @csrf
                                <div>
                                    <input type="hidden" name="classroom_id" value="">
                                </div>
                                <div class="form-floating mb-3">
                                    <input type="text" class="form-control" id="name" name="name" placeholder="Tên lớp" value="">
                                    <label for="name">Tên lớp học</label>
                                </div>
                                <div class="form-floating mb-3">
                                    <textarea class="form-control" placeholder="Miêu tả lớp học" name="description" id="description" style="height: 150px;"></textarea>
                                    <label for="description">Miêu tả lớp học</label>
                                </div>
                                <div class="form-floating mb-3">
                                    <input type="color" class="form-control bg-dark" id="theme_color" name="theme_color" placeholder="Màu chủ đề" value="#ffffff">
                                    <label for="theme_color">Màu chủ đề của lớp học</label>
                                </div>
                                <div class="form-floating mb-3">
                                    <label for="description">Ảnh đại diện của lớp</label>
                                    <input type="file" accept=".png, .jpg" onchange="loadFile(event)" id="image" name="image" >
                                    <img id="output" class="img-thumbnail" style="max-height: 150px"/>
                                </div>
                                <button class="form-floating btn btn-success" type="submit">+ Thêm</button>
                            </form>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <script>
        var loadFile = function(event) {
            var output = document.getElementById('output');
            output.src = URL.createObjectURL(event.target.files[0]);
            output.onload = function() {
                URL.revokeObjectURL(output.src) // free memory
            }
        };
    </script>
    <!-- Blank End -->
@stop
