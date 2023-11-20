<div>
    {{-- Nothing in the world is as soft and yielding as water. --}}
    <div class="container-fluid pt-4 px-4">
        <div class="bg-secondary text-center rounded p-4">
            <div class="d-flex align-items-center justify-content-between mb-4">
                <h3 class="mb-0">Quản lý người dùng</h3>
            </div>
            <div class=" mb-3">
                <div class="text-start text-white my-2">
                    Tìm kiếm dựa trên tên hoặc tài khoản
                </div>
                <input wire:model.live.debounce.250ms="query" type="text" class="form-control">
            </div>
            <div class="table-responsive">
                <table class="table text-start align-middle table-bordered table-hover mb-0">
                    <thead>
                        <tr class="text-white">
                            <th scope="col">Tên người dùng</th>
                            <th scope="col">Tài khoản</th>
                            <th scope="col">Trạng thái</th>
                            <th scope="col">Sửa</th>
                        </tr>
                    </thead>
                    <tbody>
                        @foreach ($users as $user)
                            <tr>
                                <td style="max-width: 20px;">{{ $user->name }}</td>
                                <td class="text-truncate w-25">{{ $user->username }}</td>
                                <td class="text-truncate w-25">
                                    @if ($user->is_ban)
                                        <div class="text-danger">
                                            Bị chặn
                                        </div>
                                    @else
                                        <div class="text-success">
                                            bình thường
                                        </div>
                                    @endif
                                </td>


                                <td class="text-center"><a class="btn btn-sm btn-primary" href="{{ route('admin.user.show', ['user_id' => $user->id]) }}">Sửa</a></td>
                            </tr>
                        @endforeach
                    </tbody>
                </table>
            </div>
            <div class="mt-3">
                {{ $users->links() }}
            </div>
        </div>
    </div>


</div>
