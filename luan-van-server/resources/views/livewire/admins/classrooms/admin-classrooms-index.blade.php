<div>
    <div class="container-fluid pt-4 px-4">
        <div class="row bg-secondary rounded justify-content-center mx-0">
            <div class="row text-center">
                <div class="col-12">
                    <div class="bg-secondary rounded p-4 ">
                        <h3 class="mb-4">Các lớp học</h3>
                        <div class="table-responsive ">
                            <table class="table ">
                                <thead>
                                    <tr>
                                        <th scope="col">ID</th>
                                        <th scope="col">Tên lớp</th>
                                        <th scope="col">Miêu tả lớp học</th>
                                        <th scope="col">Trạng thái</th>
                                        <th scope="col">Chỉnh sửa</th>
                                    </tr>
                                </thead>
                                @foreach ($classrooms as $classroom)
                                    <tbody>
                                        <tr>
                                            <th class="align-middle" scope="row">{{ $classroom->id }}</th>
                                            <td class="align-middle w-25" >{{ $classroom->name }}</td>
                                            <td class="align-middle w-25" style="white-space: pre-line">{{ $classroom->description }}</td>
                                            <td class="align-middle w-25">
                                                @if ($classroom->is_open)
                                                    <div class="text-success">
                                                        Đang mở
                                                    </div>
                                                @else
                                                    <div class="text-danger">
                                                        Đang đóng
                                                    </div>
                                                @endif
                                            </td>
                                            <td class="align-middle">
                                                <a href="{{ route('classrooms.show', ['id' => $classroom->id]) }}" class="btn btn-info">Chỉnh sửa</a>
                                            </td>
                                        </tr>
                                    </tbody>
                                @endforeach
                            </table>
                        </div>
                    </div>
                    {{ $classrooms->onEachSide(1)->links() }}
                </div>
            </div>
        </div>
    </div>
</div>
