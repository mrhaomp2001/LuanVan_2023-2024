<div>
    <div class="container-fluid pt-4 px-4">
        <div class="row bg-secondary rounded justify-content-center mx-0">
            <div class="row text-center">
                <div class="col-12">
                    <div class="bg-secondary rounded p-4 ">
                        <h2 class="text-start">Quản lý người quản trị nội dung</h2>
                        <hr />
                        <div>
                            <h5 class="text-start">
                                Những người quản trị nội dung hiện tại
                            </h5>
                            <div class="table-responsive">
                                <table class="table text-start align-middle table-bordered table-hover mb-0">
                                    <thead>
                                        <tr class="text-white">
                                            <th scope="col">Tên người dùng</th>
                                            <th scope="col">Tài khoản</th>
                                            <th scope="col">Xóa quyền</th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        @foreach ($moderators as $moderator)
                                            <tr>
                                                <td style="max-width: 20px;">{{ $moderator->name }}</td>
                                                <td class="text-truncate" style="max-width: 400px;">{{ $moderator->username }}</td>

                                                <td><button wire:click="deletePermissions({{ $moderator->id }})"  wire:confirm="Are you sure?"  class="btn btn-sm btn-danger" href="">Xóa quyền</button ></td>
                                            </tr>
                                        @endforeach
                                    </tbody>
                                </table>
                            </div>
                        </div>

                        <hr />
                        <div>
                            <h5 class="text-start">
                                Cấp quyền cho người dùng làm quản trị viên
                            </h5>
                            <h6 class="text-start text-light">
                                Tìm người dùng bằng tên tài khoản, hoặc tên người dùng
                            </h6>
                            <div class=" mb-3">
                                <input wire:model.live.debounce.250ms="searchQuery" type="text" class="form-control"value="">
                                @error('answers.0')
                                    <p class="text-start text-danger">
                                        {{ $message }}
                                    </p>
                                @enderror
                            </div>
                            <div class="table-responsive">
                                <table class="table text-start align-middle table-bordered table-hover mb-0">
                                    <thead>
                                        <tr class="text-white">
                                            <th scope="col">Tên người dùng</th>
                                            <th scope="col">Tài khoản</th>
                                            <th scope="col">Cấp quyền</th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        @foreach ($users as $user)
                                            <tr>
                                                <td style="max-width: 20px;">{{ $user->name }}</td>
                                                <td class="text-truncate" style="max-width: 400px;">{{ $user->username }}</td>

                                                <td><button wire:click="grantPermissions({{ $user->id }})"  wire:confirm="Are you sure?" class="btn btn-sm btn-outline-success">Cấp quyền</button></td>
                                            </tr>
                                        @endforeach
                                    </tbody>
                                </table>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</div>
